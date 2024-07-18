using InstagramLiveNetApi.Exceptions;
using InstagramLiveNetApi.Http;
using InstagramLiveNetApi.Http.Requests;
using Newtonsoft.Json.Linq;

namespace InstagramLiveNet.Http.Routes;


public class UserRoute : HttpRoute<UserRequest, UserResponse>
{
    private readonly LiveHttpClient _client;

    public UserRoute(LiveHttpClient client)
    {
        _client = client;
    }


    public override async Task<UserResponse> Execute(UserRequest input)
    {
        var response = await _client.GetAsync(
            $"https://www.instagram.com/api/v1/users/web_profile_info/?username=${input.Username}");

        response = await CheckLogin(response);

        var responseData = await response.Content.ReadAsStringAsync();
        var json = JObject.Parse(responseData);
        var userId = json["data"]?["user"]?["id"]?.ToString();

        if (string.IsNullOrEmpty(userId))
        {
            throw new LiveClientHttpException($"Failed to fetch user ID: ${response.StatusCode} - ${responseData}");
        }

        return new UserResponse()
        {
            UserId = userId,
        };
    }
}