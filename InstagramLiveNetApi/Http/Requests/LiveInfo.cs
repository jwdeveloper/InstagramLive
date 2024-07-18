namespace InstagramLiveNetApi.Http.Requests;


public class LiveInfoRequest
{
    public string UserId { get; set; }
}

public class LiveInfoResponse
{
    public string Id { get; set; }
    public long PublishedTime { get; set; }
    public string BroadcastPrompt { get; set; }
    public string BroadcastMessage { get; set; }
    public Dimensions Dimensions { get; set; }
    public Dimensions DimensionsTyped { get; set; }
    public bool HideFromFeedUnit { get; set; }
    public bool IsLiveCommentMentionEnabled { get; set; }
    public bool IsLiveCommentRepliesEnabled { get; set; }
    public string MediaId { get; set; }
    public long ResponseTimestamp { get; set; }
    public string StrongId { get; set; }
    public BroadcastOwner BroadcastOwner { get; set; }
    public string CoverFrameUrl { get; set; }
    public int VideoDuration { get; set; }
    public bool InternalOnly { get; set; }
    public bool IsViewerCommentAllowed { get; set; }
    public Dictionary<string, object> BroadcastExperiments { get; set; }
    public string LivePostId { get; set; }
    public int IsPlayerLiveTraceEnabled { get; set; }
    public int Visibility { get; set; }
    public Dictionary<string, object> MediaOverlayInfo { get; set; }
    public List<object> Cobroadcasters { get; set; }
    public string OrganicTrackingToken { get; set; }
    public string DashPlaybackUrl { get; set; }
    public string DashAbrPlaybackUrl { get; set; }
    public int ViewerCount { get; set; }
    public BroadcastStatus BroadcastStatus { get; set; }
    public string Status { get; set; }
}

public enum BroadcastStatus
{
    ACTIVE,
    STOPPED,
    INTERRUPTED
}

public class Dimensions
{
    public int Height { get; set; }

    public int Width { get; set; }
}

public class BroadcastOwner
{
    public string Pk { get; set; }
    public string PkId { get; set; }
    public string FullName { get; set; }
    public bool IsPrivate { get; set; }
    public string StrongId { get; set; }
    public string Username { get; set; }
    public bool IsVerified { get; set; }
    public string LiveBroadcastId { get; set; }
    public int LiveBroadcastVisibility { get; set; }
    public string ProfilePicId { get; set; }
    public string ProfilePicUrl { get; set; }
    public string LiveSubscriptionStatus { get; set; }
    public string InteropMessagingUserFbid { get; set; }
    public FriendshipStatus FriendshipStatus { get; set; }
}

public class FriendshipStatus
{
    public bool Following { get; set; }
    public bool FollowedBy { get; set; }
    public bool Blocking { get; set; }
    public bool Muting { get; set; }
    public bool IsPrivate { get; set; }
    public bool IncomingRequest { get; set; }
    public bool OutgoingRequest { get; set; }
    public bool IsBestie { get; set; }
    public bool IsRestricted { get; set; }
    public bool IsFeedFavorite { get; set; }
    public bool Subscribed { get; set; }
    public bool IsEligibleToSubscribe { get; set; }
}