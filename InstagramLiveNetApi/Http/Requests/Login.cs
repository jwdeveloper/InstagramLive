namespace InstagramLiveNetApi.Http.Requests;


public class LoginRequest
{
    public string SessionId { get; set; }
    public string DeviceId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginResponse
{
    public bool Authenticated { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }

    public string UserId { get; set; }

    public bool OneTapPrompt { get; set; }

    public bool HasOnboardedToTextPostApp { get; set; }
}