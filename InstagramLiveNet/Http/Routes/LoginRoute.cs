using InstagramLiveNetApi.Exceptions;
using InstagramLiveNetApi.Http;
using InstagramLiveNetApi.Http.Requests;
using Newtonsoft.Json;

namespace InstagramLiveNet.Http.Routes;


public class LoginRoute : HttpRoute<LoginRequest, LoginResponse>
{
    private readonly LiveHttpClient _client;

    public LoginRoute(LiveHttpClient client)
    {
        _client = client;
    }

    public override async Task<LoginResponse> Execute(LoginRequest input)
    {
        // Set Base Cookies
        _client.SetCookie("sessionid", input.SessionId ?? string.Empty);
        _client.SetCookie("ig_did", input.DeviceId);
        _client.SetCookie("mid", string.Empty);
        _client.SetCookie("ig_pr", "1");
        _client.SetCookie("ig_vw", "1920");
        _client.SetCookie("ig_cb", "1");
        _client.SetCookie("csrftoken", string.Empty);
        _client.SetCookie("s_network", string.Empty);
        _client.SetCookie("db_user_id", string.Empty);

        // Request Instagram's base URL to get the csrftoken cookie
        var result = await _client.GetAsync("https://www.instagram.com/");
        result.EnsureSuccessStatusCode();


        // Get the CSRF Token
        var csrfToken = _client.GetCookie("csrftoken");
        _client.SetDefaultRequestHeader("X-CSRFToken", csrfToken);


        // If they pass session ID we can just skip actually logging in
        if (!string.IsNullOrEmpty(input.SessionId))
        {
            return new LoginResponse()
            {
                Authenticated = true,
                Message = "Already login",
                Status = "ok",
                UserId = "0",
                OneTapPrompt = false,
                HasOnboardedToTextPostApp = false,
            };
        }


        if (string.IsNullOrEmpty(input.Username) || string.IsNullOrEmpty(input.Password))
        {
            throw new LiveClientHttpException(
                "Username and password are required for login if session ID is not passed.");
        }

        // Sleep for a random amount of time
        await Task.Delay(new Random().Next(0, 2000));
        var encPassword = $"#PWD_INSTAGRAM_BROWSER:0:{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}:{input.Password}";

        var requestData = new Dictionary<string, string>
        {
            { "username", input.Username },
            { "enc_password", encPassword }
        };

        var requestContent = new FormUrlEncodedContent(requestData);

        var response = await _client.PostAsync("https://www.instagram.com/accounts/login/ajax/", requestContent);
        var responseData = await response.Content.ReadAsStringAsync();

        var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseData);

        // Handle 2FA error
        if (responseData.Contains("two_factor_required"))
        {
            throw new LiveClientHttpException("Two factor authentication required!");
        }

        // Handle checkpoint error
        if (responseData.Contains("checkpoint_url"))
        {
            throw new LiveClientHttpException(
                $"Checkpoint required. {loginResponse.Message} - follow the instructions, then retry.");
        }

        if (loginResponse.Status != "ok")
        {
            if (!string.IsNullOrEmpty(loginResponse.Message))
            {
                throw new LiveClientHttpException(
                    $"Login error: \"{loginResponse.Status}\" status, message \"{loginResponse.Message}\"");
            }
            else
            {
                throw new LiveClientHttpException($"Login error: \"{loginResponse.Status}\" status.");
            }
        }

        if (loginResponse.Authenticated != true)
        {
            if (responseData.Contains("user"))
            {
                throw new LiveClientHttpException(
                    $"Login error: Wrong password, or too many attempts? Response: {response.StatusCode} - {responseData}");
            }
            else
            {
                throw new LiveClientHttpException($"Login error: User {input.Username} does not exist.");
            }
        }

        csrfToken = _client.GetCookie("csrftoken");
        _client.SetDefaultRequestHeader("X-CSRFToken", csrfToken);
        return loginResponse;
    }
}