<div align="center" >
<a target="blank" >
<img src="https://raw.githubusercontent.com/jwdeveloper/TikTokLiveJava/develop-1_0_0/Tools-ReadmeGenerator/src/main/resources/logo.svg" width="15%" >
</a>
</div>
<div align="center" >
<h1>Instagram Live</h1>

❤️❤️🎁 *Connect to Instagram live in 3 lines* 🎁❤️❤️

<div align="center" >
<a href="https://jitpack.io/#jwdeveloper/TikTok-Live-Java" target="blank" >
<img src="https://jitpack.io/v/jwdeveloper/TikTok-Live-Java.svg" width="20%" >
</a>


<a href="https://discord.gg/e2XwPNTBBr" target="blank" >
<img src="https://img.shields.io/badge/Discord-%235865F2.svg?style=for-the-badge&logo=discord&logoColor=white" >
</a>

<a target="blank" >
<img src="https://img.shields.io/badge/java-%23ED8B00.svg?style=for-the-badge&logo=openjdk&logoColor=white" >
</a>
</div>
</div>

# Introduction



Join the support [discord](https://discord.gg/e2XwPNTBBr) and visit the `#instagram-support` channel for questions, contributions and ideas. Feel free to make pull requests with missing/new features, fixes, etc

**NOTE:** This is not an official API. It's a reverse engineering project.

#### Overview
- [Getting started](#getting-started)
- [Example](#example)
- [Contributing](#contributing)

## Getting started

1. Install the package

Maven
```xml
   <repositories>
        <repository>
            <id>jitpack.io</id>
            <url>https://jitpack.io</url>
        </repository>
    </repositories>

   <dependencies>
         <dependency>
            <groupId>com.github.jwdeveloper.TikTok-Live-Java</groupId>
            <artifactId>Client</artifactId>
             <version>1.8.0-Release</version>
            <scope>compile</scope>
        </dependency>
   </dependencies>
```

2. Create your first chat connection

```C#

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
```

## Contributing

[Library documentation for contributors](https://github.com/jwdeveloper/TikTokLiveJava/wiki)

Your improvements are welcome! Feel free to open an <a href="https://github.com/jwdeveloper/TikTok-Live-Java/issues">issue</a> or <a href="https://github.com/jwdeveloper/TikTok-Live-Java/pulls">pull request</a>.
