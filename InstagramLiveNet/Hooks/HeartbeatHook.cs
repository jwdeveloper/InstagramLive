using InstagramLiveNetApi;
using InstagramLiveNetApi.Events;
using InstagramLiveNetApi.Http.Requests;

namespace InstagramLiveNet.Hooks;

public class HeartbeatHook : ILiveHook
{
    public async Task Execute(ILiveClient client, ILiveRouter router, CancellationToken ctx)
    {
        var response = await router.Heartbeat(new HeartbeatRequest()
        {
            LiveId = client.Properties.TargetUserId
        });

        client.PublishEvent(new LiveHeartbeatEvent(response));

        if (response.BroadcastStatus == BroadcastStatus.STOPPED)
        {
            client.Disconnect();
        }
    }
}