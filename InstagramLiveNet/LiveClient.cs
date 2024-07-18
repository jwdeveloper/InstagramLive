using InstagramLiveNet.Hooks;
using InstagramLiveNet.Http;
using InstagramLiveNet.Http.Routes;
using InstagramLiveNetApi;
using InstagramLiveNetApi.Enums;
using InstagramLiveNetApi.Events;
using InstagramLiveNetApi.Exceptions;
using InstagramLiveNetApi.Http.Requests;

namespace InstagramLiveNet;

public class LiveClient : ILiveClient
{
    public LiveClientProperties Properties { get; }
    public LiveClientStatistics Statistics { get; }
    public ILiveRouter Router { get; }

    private readonly LiveEventsBus _liveEventsBus;
    private readonly LiveHooks _liveHooks;

    private CancellationTokenSource _source;

    public LiveClient(
        LiveClientProperties properties,
        LiveEventsBus liveEventsBus,
        LiveRouter liveRouter,
        LiveHooks liveHooks)
    {
        Properties = properties;
        Router = liveRouter;
        _liveEventsBus = liveEventsBus;
        _liveHooks = liveHooks;
        _source = new CancellationTokenSource();
    }

    public async Task Login()
    {
        var response = await Router.Login(new LoginRequest()
        {
            Password = Properties.Password,
            Username = Properties.UserName,
            DeviceId = Properties.DeviceId,
            SessionId = Properties.SessionId
        });
        Console.WriteLine(response);
    }

    public async Task Connect(string userNameOrUrl)
    {
        try
        {
            await TryConnect(userNameOrUrl);
        }
        catch (LiveClientException e)
        {
            Disconnect();
            _liveEventsBus.Publish(this, new LiveErrorEvent(e));
        }
        catch (Exception e)
        {
            Disconnect();
            throw new Exception("Live client unexpected exception, contact library maintainer", e);
        }
    }

    private async Task TryConnect(string userName)
    {
        if (Properties.State == State.Connecting)
        {
            throw new LiveClientException("Client is already connecting to live!");
        }

        if (Properties.State == State.Connected)
        {
            Disconnect();
        }

        Properties.State = State.Connecting;
        _liveEventsBus.Publish(this, new LiveConnectingEvent());

        if (string.IsNullOrEmpty(Properties.SessionId))
        {
            await Login();
        }

        if (string.IsNullOrEmpty(Properties.TargetUserId))
        {
            Properties.TargetUserId = await GetUserInfo(userName);
        }

        await GetLiveInfo();

        _liveEventsBus.Publish(this, new LiveConnectedEvent());
        Properties.State = State.Connected;


        var token = _source.Token;
        var timeout = 5000;
        var hooks = new List<ILiveHook>()
        {
            new CommentsHook(),
            new HeartbeatHook(),
        };

        while (!token.IsCancellationRequested)
        {
            foreach (var hook in hooks)
            {
                token.ThrowIfCancellationRequested();
                await hook.Execute(this, Router, token);
            }

            await Task.Delay(timeout, token);
        }
    }


    private async Task<string> GetUserInfo(string userName)
    {
        var userResponse = await Router.User(new UserRequest()
        {
            Username = userName
        });
        return userResponse.UserId;
    }

    private async Task GetLiveInfo()
    {
        var infoResponse = await Router.LiveInfo(new LiveInfoRequest()
        {
            UserId = Properties.TargetUserId
        });
    }


    public void PublishEvent<T>(T @event) where T : LiveEvent
    {
        _liveEventsBus.Publish(this, @event);
    }

    public void Disconnect()
    {
        _source.Cancel();
        Properties.State = State.Disconnected;
    }
}