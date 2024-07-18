using InstagramLiveNetApi.Exceptions;

namespace InstagramLiveNetApi.Events;

public class LiveErrorEvent : LiveEvent
{
    public LiveClientException Exception { get; }

    public LiveErrorEvent(LiveClientException exception)
    {
        Exception = exception;
    }
}