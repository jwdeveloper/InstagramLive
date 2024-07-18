using InstagramLiveNet;
using InstagramLiveNetApi;

var client = InstagramLive
    .NewClient()
    .Configure(properties =>
    {
        //User credentials to login
        properties.UserName = "username";
        properties.Password = "********";

        //Or sessionId + deviceId
        properties.SessionId = "session id";
        properties.DeviceId = "device id    ";
    })
    .OnError((live, @event) =>
    {
        Console.WriteLine($"we have error { @event.Exception.Message}");
    })
    .OnConnected((live, @event) =>
    {
        Console.WriteLine("Connected to live");
    })
    .OnDisconnected((liveClient, data) =>
    {
        Console.WriteLine("OnDisconnected to live");
    })
    .OnJoin((liveClient, data) =>
    {
        Console.WriteLine("User joined to live "+data.ToJson());
    })
    .OnComment((live, @event) =>
    {
        Console.WriteLine("Comment "+@event.ToJson());
    })
    .OnSystemComment((liveClient,  @event) =>
    {
        Console.WriteLine("System Comment "+@event.ToJson());
    })
    .Build();




await client.Connect("jacolwol");