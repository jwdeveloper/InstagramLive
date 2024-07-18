using InstagramLiveNetApi.Http;
using InstagramLiveNetApi.Http.Requests;

namespace InstagramLiveNet.Http.Routes;

public class HeartbeatRoute : HttpRoute<HeartbeatRequest, HeartbeatResponse>
{
    private LiveHttpClient _liveHttpClient;

    public HeartbeatRoute(LiveHttpClient liveHttpClient)
    {
        _liveHttpClient = liveHttpClient;
    }

    public async override Task<HeartbeatResponse> Execute(HeartbeatRequest input)
    {
        var fetchUrl = "https://www.instagram.com/api/v1/live/${config.live_id}/heartbeat_and_get_viewer_count/";

        var request = await _liveHttpClient.GetAsync(fetchUrl);
        request = await CheckLogin(request);
        return await ToJson<HeartbeatResponse>(request);
    }
}