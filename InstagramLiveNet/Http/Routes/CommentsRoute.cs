using InstagramLiveNetApi.Exceptions;
using InstagramLiveNetApi.Http;
using InstagramLiveNetApi.Http.Requests;
using Newtonsoft.Json.Linq;

namespace InstagramLiveNet.Http.Routes;

public class CommentsRoute : HttpRoute<CommentsRequest, CommentsResponse>
{
    private readonly LiveHttpClient _client;

    public CommentsRoute(LiveHttpClient client)
    {
        _client = client;
    }


    public override async Task<CommentsResponse> Execute(CommentsRequest input)
    {
        var lastCommentParam =
            string.IsNullOrEmpty(input.LastCommentTimeStamp)
                ? string.Empty
                : $"?last_comment_ts = ${input.LastCommentTimeStamp}";
        var fetchUrl = $"https://www.instagram.com/api/v1/live/${input.LiveId}/get_comment/${lastCommentParam}";
        var response = await _client.GetAsync(fetchUrl);

        response = await CheckLogin(response);
        var responseObject = await ToJson<CommentsResponse>(response);
        return responseObject;
    }
}
