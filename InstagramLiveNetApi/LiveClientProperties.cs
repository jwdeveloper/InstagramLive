using InstagramLiveNetApi.Enums;

namespace InstagramLiveNetApi;

public class LiveClientProperties
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string SessionId { get; set; }
    public string DeviceId { get; set; }
    public string TargetUserId { get; set; }

    public State State { get; set; } = State.Disconnected;
}