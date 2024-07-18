using InstagramLiveNetApi;
using InstagramLiveNetApi.Events;
using InstagramLiveNetApi.Http.Requests;

namespace InstagramLiveNet.Hooks;

public class CommentsHook : ILiveHook
{
    private string _lastCommentTimestamp = string.Empty;

    public async Task Execute(ILiveClient client, ILiveRouter router, CancellationToken ctx)
    {
        var comments = await router.Comments(new CommentsRequest()
        {
            LiveId = client.Properties.TargetUserId,
            LastCommentTimeStamp = _lastCommentTimestamp
        });


        if (comments.Comments.Count > 0 || comments.SystemComments.Count > 0)
        {
            _lastCommentTimestamp = ((int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString();
        }

        foreach (var comment in comments.Comments)
        {
            client.PublishEvent(new LiveUserCommentEvent(comment));
        }

        foreach (var systemComment in comments.SystemComments)
        {
            client.PublishEvent(new LiveSystemCommentEvent(systemComment));
            switch (systemComment.Type)
            {
                case SystemCommentType.multi_user_joined:
                    client.PublishEvent(new LiveUserJoinEvent(systemComment.User));
                    break;
            }
        }
    }
}