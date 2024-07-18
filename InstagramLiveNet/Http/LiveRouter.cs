using InstagramLiveNet.Http.Routes;
using InstagramLiveNetApi;
using InstagramLiveNetApi.Http.Requests;

namespace InstagramLiveNet.Http;

public class LiveRouter : ILiveRouter
{
    private readonly LoginRoute _loginRoute;
    private readonly UserRoute _userRoute;
    private readonly LiveInfoRoute _liveInfoRoute;
    private readonly CommentsRoute _commentsRoute;
    private readonly HeartbeatRoute _heartbeatRoute;

    public LiveRouter(LiveHttpClient client)
    {
        _loginRoute = new LoginRoute(client);
        _userRoute = new UserRoute(client);
        _liveInfoRoute = new LiveInfoRoute(client);
        _commentsRoute = new CommentsRoute(client);
        _heartbeatRoute = new HeartbeatRoute(client);
    }

    public async Task<CommentsResponse> Comments(CommentsRequest request)
    {
        return await _commentsRoute.Execute(request);
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        return await _loginRoute.Execute(request);
    }

    public async Task<UserResponse> User(UserRequest request)
    {
        return await _userRoute.Execute(request);
    }

    public async Task<LiveInfoResponse> LiveInfo(LiveInfoRequest request)
    {
        return await _liveInfoRoute.Execute(request);
    }

    public async Task<HeartbeatResponse> Heartbeat(HeartbeatRequest request)
    {
        return await _heartbeatRoute.Execute(request);
    }
}