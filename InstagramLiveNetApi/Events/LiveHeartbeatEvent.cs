using InstagramLiveNetApi.Http.Requests;

namespace InstagramLiveNetApi.Events;

public class LiveHeartbeatEvent : LiveEvent
{
    private HeartbeatResponse Heartbeat { get; }

    public LiveHeartbeatEvent(HeartbeatResponse heartbeat)
    {
        Heartbeat = heartbeat;
    }
}