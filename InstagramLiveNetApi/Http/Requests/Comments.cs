namespace InstagramLiveNetApi.Http.Requests;

public class CommentsRequest
{
    public string LiveId { get; set; }
    public string LastCommentTimeStamp { get; set; }
}

public class CommentsResponse
{
    public bool CommentLikesEnabled { get; set; }
    public List<UserComment> Comments { get; set; }
    public int CommentCount { get; set; }
    public string Caption { get; set; }
    public bool CaptionIsEdited { get; set; }
    public bool HasMoreComments { get; set; }
    public bool HasMoreHeadloadComments { get; set; }
    public string MediaHeaderDisplay { get; set; }
    public bool CanViewMorePreviewComments { get; set; }
    public int LiveSecondsPerComment { get; set; }
    public string IsFirstFetch { get; set; }
    public List<SystemComment> SystemComments { get; set; }
    public int CommentMuted { get; set; }
    public bool IsViewerCommentAllowed { get; set; }
    public string Status { get; set; }
}

public class UserComment
{
    public string Pk { get; set; }
    public string UserId { get; set; }
    public int Type { get; set; }
    public bool DidReportAsSpam { get; set; }
    public long CreatedAt { get; set; }
    public long CreatedAtUtc { get; set; }
    public long CreatedAtForFbApp { get; set; }
    public string ContentType { get; set; }
    public string Status { get; set; }
    public int BitFlags { get; set; }
    public bool ShareEnabled { get; set; }
    public bool IsRankedComment { get; set; }
    public string MediaId { get; set; }
    public User User { get; set; }
    public string Text { get; set; }
    public bool IsCovered { get; set; }
    public bool HasLikedComment { get; set; }
    public int CommentLikeCount { get; set; }
}

public class User
{
    public string Pk { get; set; }
    public string PkId { get; set; }
    public string Id { get; set; }
    public string FullName { get; set; }
    public bool IsPrivate { get; set; }
    public bool HasOnboardedToTextPostApp { get; set; }
    public string StrongId { get; set; }
    public string FbidV2 { get; set; }
    public string Username { get; set; }
    public bool IsVerified { get; set; }
    public string ProfilePicId { get; set; }
    public string ProfilePicUrl { get; set; }
    public bool IsMentionable { get; set; }
    public long LatestReelMedia { get; set; }
    public long LatestBestiesReelMedia { get; set; }
}

public class SystemComment
{
    public string Pk { get; set; }
    public long CreatedAt { get; set; }
    public User User { get; set; }
    public string Text { get; set; }
    public int UserCount { get; set; }

    public bool HasSocialContext { get; set; }
    public SystemCommentType Type { get; set; }
}

public enum SystemCommentType
{
    multi_user_joined
}