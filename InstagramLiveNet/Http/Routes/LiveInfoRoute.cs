using InstagramLiveNetApi.Http;
using InstagramLiveNetApi.Http.Requests;
using Newtonsoft.Json;

namespace InstagramLiveNet.Http.Routes;

public class LiveInfoRoute : HttpRoute<LiveInfoRequest, LiveInfoResponse>
{
    private readonly LiveHttpClient _client;

    public LiveInfoRoute(LiveHttpClient client)
    {
        _client = client;
    }

    public override async Task<LiveInfoResponse> Execute(LiveInfoRequest input)
    {
        var url = $"https://www.instagram.com/api/v1/live/${input.UserId}/heartbeat_and_get_viewer_count/";
        var response = await _client.GetAsync(url);
        var responseData = await response.Content.ReadAsStringAsync();
        var responseObject = JsonConvert.DeserializeObject<LiveInfoResponse>(responseData);
        return responseObject;
    }
}
