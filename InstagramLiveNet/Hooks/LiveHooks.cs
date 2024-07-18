using InstagramLiveNet.Http;
using InstagramLiveNetApi;
using InstagramLiveNetApi.Events;
using InstagramLiveNetApi.Exceptions;

namespace InstagramLiveNet.Hooks;

public delegate Task HookAction(ILiveClient client, ILiveRouter router, CancellationToken ctx);

public interface ILiveHook
{
    public Task Execute(ILiveClient client, ILiveRouter router, CancellationToken ctx);
}

public class DelegateLiveHook : ILiveHook
{
    private readonly HookAction _hookAction;

    public DelegateLiveHook(HookAction hookAction)
    {
        _hookAction = hookAction;
    }


    public async Task Execute(ILiveClient client, ILiveRouter router, CancellationToken ctx)
    {
        await _hookAction(client, router, ctx);
    }
}

public class LiveHooks
{
    private readonly List<System.Timers.Timer> _hooks = new();
    private readonly LiveEventsBus _eventsBus;
    private readonly ILiveRouter _router;

    private ILiveClient? _liveClient;
    private CancellationToken _ctx;

    public LiveHooks(ILiveRouter router, LiveEventsBus eventsBus)
    {
        _router = router;
        _eventsBus = eventsBus;
    }


    public void AddHook(ILiveHook hook, int timeout)
    {
        var timer = new System.Timers.Timer(timeout);

        timer.Elapsed += (sender, args) =>
        {
            try
            {
                if (_liveClient is null || _router is null)
                    return;

                hook.Execute(_liveClient, _router, _ctx);
            }
            catch (Exception e)
            {
                var exception = new LiveClientException("Error while executing hook", e);
                _eventsBus.Publish(_liveClient, new LiveErrorEvent(exception));
            }
        };
        _hooks.Add(timer);
    }

    public void AddHook(HookAction hook, int timeout)
    {
        AddHook(new DelegateLiveHook(hook), timeout);
    }

    public void Start(ILiveClient liveClient, CancellationToken ctx)
    {
        _liveClient = liveClient;
        this._ctx = ctx;
        foreach (var hook in _hooks)
        {
            hook.Start();
        }
    }

    public void Reset()
    {
        foreach (var hook in _hooks)
        {
            hook.Stop();
        }

        _hooks.Clear();
    }
}