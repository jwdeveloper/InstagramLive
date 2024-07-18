using InstagramLiveNetApi.Http.Requests;

namespace InstagramLiveNetApi;

public interface ILiveRouter
{
    public Task<CommentsResponse> Comments(CommentsRequest request);
    public Task<LoginResponse> Login(LoginRequest request);
    public Task<UserResponse> User(UserRequest request);
    public Task<LiveInfoResponse> LiveInfo(LiveInfoRequest request);
    public Task<HeartbeatResponse> Heartbeat(HeartbeatRequest request);
}