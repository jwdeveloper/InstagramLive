using InstagramLiveNetApi;
using InstagramLiveNetApi.Events;

namespace InstagramLiveNet;

public class LiveEventsBus
{
    private readonly Dictionary<Type, List<Delegate>> _subscribers = new Dictionary<Type, List<Delegate>>();

    public void Subscribe<TEventType>(LiveEventAction<TEventType> action) where TEventType : LiveEvent
    {
        var eventType = typeof(TEventType);

        if (!_subscribers.ContainsKey(eventType))
        {
            _subscribers[eventType] = new List<Delegate>();
        }

        _subscribers[eventType].Add(action);
    }

    public void Unsubscribe<TEventType>(LiveEventAction<TEventType> action) where TEventType : LiveEvent
    {
        var eventType = typeof(TEventType);

        if (_subscribers.ContainsKey(eventType))
        {
            _subscribers[eventType].Remove(action);

            // Clean up if no more subscribers for this event type
            if (_subscribers[eventType].Count == 0)
            {
                _subscribers.Remove(eventType);
            }
        }
    }

    public void Publish<TEventType>(ILiveClient liveClient, TEventType eventItem) where TEventType : LiveEvent
    {
        var eventType = typeof(TEventType);

        if (_subscribers.ContainsKey(eventType))
        {
            foreach (var subscriber in _subscribers[eventType])
            {
                ((LiveEventAction<TEventType>)subscriber)(liveClient, eventItem);
            }
        }
    }
}