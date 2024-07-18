using InstagramLiveNetApi.Events;

namespace InstagramLiveNetApi;

/// <summary>
/// Defines a client that interacts with Instagram Live services.
/// </summary>
public interface ILiveClient
{
    /// <summary>
    /// Asynchronously logs the client into the Instagram Live service.
    /// </summary>
    /// <returns>A task that represents the asynchronous login operation.</returns>
    public Task Login();

    /// <summary>
    /// Asynchronously connects the client to the specified user's Instagram Live session.
    /// </summary>
    /// <param name="userNameOrUrl">The username of the Instagram account to connect to. Alternatively you can pass live URL</param>
    /// <returns>A task that represents the asynchronous connect operation.</returns>
    public Task Connect(string userNameOrUrl);

    /// <summary>
    /// Asynchronously disconnects the client from the current Instagram Live session.
    /// </summary>
    /// <returns>A task that represents the asynchronous disconnect operation.</returns>
    public void Disconnect();


    public void PublishEvent<T>(T @event) where T : LiveEvent;


    /// <summary>
    /// Gets the properties of the live client, providing various settings and information related to the client's current state.
    /// </summary>
    public LiveClientProperties Properties { get; }

    /// <summary>
    /// Gets the statistics of the live client, providing various metrics and statistics related to the client's performance and activities.
    /// </summary>
    public LiveClientStatistics Statistics { get; }

    public ILiveRouter Router { get; }
}