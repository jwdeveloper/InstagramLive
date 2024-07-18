using InstagramLiveNetApi.Http.Requests;

namespace InstagramLiveNetApi.Events;

public class LiveSystemCommentEvent : LiveEvent
{
    public SystemComment Comment { get; }

    public LiveSystemCommentEvent(SystemComment comment)
    {
        Comment = comment;
    }
}