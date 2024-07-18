using InstagramLiveNetApi.Http.Requests;

namespace InstagramLiveNetApi.Events;

public class LiveUserCommentEvent : LiveEvent
{
    public UserComment Comment { get; }
    public LiveUserCommentEvent(UserComment comment)
    {
        Comment = comment;
    }
}