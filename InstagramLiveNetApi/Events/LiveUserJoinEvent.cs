using InstagramLiveNetApi.Http.Requests;

namespace InstagramLiveNetApi.Events;

public class LiveUserJoinEvent : LiveEvent
{
    public User User { get; }

    public LiveUserJoinEvent(User user)
    {
        User = user;
    }
}