using InstagramLiveNetApi.Events;

namespace InstagramLiveNetApi;

/// <summary>
/// Defines a builder interface for creating instances of <see cref="ILiveClient"/>.
/// </summary>
public interface ILiveClientBuilder : ILiveClientBuilderEvents, ILiveConfigurationBuilder
{
    /// <summary>
    /// Builds and returns an instance of <see cref="ILiveClient"/>.
    /// </summary>
    /// <returns>An instance of <see cref="ILiveClient"/>.</returns>
    ILiveClient Build();
}

/// <summary>
/// Defines a configuration builder interface for <see cref="ILiveClientBuilder"/>.
/// </summary>
public interface ILiveConfigurationBuilder
{
    /// <summary>
    /// Configures the <see cref="LiveClientProperties"/> for the <see cref="ILiveClient"/>.
    /// </summary>
    /// <param name="configureAction">An action to configure the <see cref="LiveClientProperties"/>.</param>
    /// <returns>An instance of <see cref="ILiveClientBuilderEvents"/> for method chaining.</returns>
    ILiveClientBuilderEvents Configure(Action<LiveClientProperties> configureAction);
}

/// <summary>
/// Represents a delegate for handling live events.
/// </summary>
/// <typeparam name="TEventType">The type of the event data.</typeparam>
/// <param name="client">The <see cref="ILiveClient"/> instance associated with the event.</param>
/// <param name="eventData">The event data of type <typeparamref name="TEventType"/>.</param>
public delegate void LiveEventAction<TEventType>(ILiveClient client, TEventType eventData) where TEventType : LiveEvent;

/// <summary>
/// Defines an interface for handling various live client events.
/// </summary>
public interface ILiveClientBuilderEvents
{
    /// <summary>
    /// Registers an action to be executed when an event of type <typeparamref name="TEventType"/> occurs.
    /// </summary>
    /// <typeparam name="TEventType">The type of the event.</typeparam>
    /// <param name="action">The action to be executed when the event occurs.</param>
    /// <returns>An instance of <see cref="ILiveClientBuilder"/> for method chaining.</returns>
    ILiveClientBuilder OnEvent<TEventType>(LiveEventAction<TEventType> action) where TEventType : LiveEvent;

    /// <summary>
    /// Registers an action to be executed when a connected event occurs.
    /// </summary>
    /// <param name="action">The action to be executed when the connected event occurs.</param>
    /// <returns>An instance of <see cref="ILiveClientBuilder"/> for method chaining.</returns>
    ILiveClientBuilder OnConnected(LiveEventAction<LiveConnectedEvent> action) => OnEvent(action);

    /// <summary>
    /// Registers an action to be executed when a disconnected event occurs.
    /// </summary>
    /// <param name="action">The action to be executed when the disconnected event occurs.</param>
    /// <returns>An instance of <see cref="ILiveClientBuilder"/> for method chaining.</returns>
    ILiveClientBuilder OnDisconnected(LiveEventAction<LiveDisconnectedEvent> action) => OnEvent(action);

    /// <summary>
    /// Registers an action to be executed when an error event occurs.
    /// </summary>
    /// <param name="action">The action to be executed when the error event occurs.</param>
    /// <returns>An instance of <see cref="ILiveClientBuilder"/> for method chaining.</returns>
    ILiveClientBuilder OnError(LiveEventAction<LiveErrorEvent> action) => OnEvent(action);

    /// <summary>
    /// Registers an action to be executed when a join event occurs.
    /// </summary>
    /// <param name="action">The action to be executed when the join event occurs.</param>
    /// <returns>An instance of <see cref="ILiveClientBuilder"/> for method chaining.</returns>
    ILiveClientBuilder OnJoin(LiveEventAction<LiveUserJoinEvent> action) => OnEvent(action);


    /// <summary>
    /// Registers an action to be executed when a user comment event occurs.
    /// </summary>
    /// <param name="action">The action to be executed when the user comment event occurs.</param>
    /// <returns>An instance of <see cref="ILiveClientBuilder"/> for method chaining.</returns>
    ILiveClientBuilder OnComment(LiveEventAction<LiveUserCommentEvent> action) => OnEvent(action);

    /// <summary>
    /// Registers an action to be executed when a system comment event occurs.
    /// </summary>
    /// <param name="action">The action to be executed when the system comment event occurs.</param>
    /// <returns>An instance of <see cref="ILiveClientBuilder"/> for method chaining.</returns>
    ILiveClientBuilder OnSystemComment(LiveEventAction<LiveSystemCommentEvent> action) => OnEvent(action);

    /// <summary>
    /// Registers an action to be executed when a heartbeat  event occurs.
    /// </summary>
    /// <param name="action">The action to be executed when the system comment event occurs.</param>
    /// <returns>An instance of <see cref="ILiveClientBuilder"/> for method chaining.</returns>
    ILiveClientBuilder OnHeartbeat(LiveEventAction<LiveHeartbeatEvent> action) => OnEvent(action);
}