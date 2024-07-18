using InstagramLiveNet.Hooks;
using InstagramLiveNet.Http;
using InstagramLiveNetApi;
using InstagramLiveNetApi.Events;

namespace InstagramLiveNet;

public class LiveClientBuilder : ILiveClientBuilder
{
    private readonly LiveClientProperties _liveClientProperties;
    private readonly LiveEventsBus _liveEventsBus;
    private readonly LiveRouter _liveRouter;
    private readonly LiveHooks _liveHooks;


    public LiveClientBuilder()
    {
        var httpClient = new LiveHttpClient();

        _liveClientProperties = new LiveClientProperties();
        _liveEventsBus = new LiveEventsBus();
        _liveRouter = new LiveRouter(httpClient);
        _liveHooks = new LiveHooks(_liveRouter, _liveEventsBus);
    }

    public ILiveClientBuilder OnEvent<TEventType>(LiveEventAction<TEventType> action) where TEventType : LiveEvent
    {
        _liveEventsBus.Subscribe(action);
        return this;
    }


    public ILiveClientBuilderEvents Configure(Action<LiveClientProperties> configureAction)
    {
        configureAction.Invoke(_liveClientProperties);
        return this;
    }

    public ILiveClient Build()
    {
        return new LiveClient(
            _liveClientProperties, 
            _liveEventsBus,
            _liveRouter, 
            _liveHooks);
    }
}