namespace InstagramLiveNetApi.Http.Requests;

public class HeartbeatRequest
{
    public string LiveId { get; set; }
}


public class HeartbeatResponse
{
    public int ViewerCount { get; set; }
    public BroadcastStatus BroadcastStatus { get; set; }
    public List<string> CobroadcasterIds { get; set; }
    public int OffsetToVideoStart { get; set; }
    public bool UserPayMaxAmountReached { get; set; }
    public string Status { get; set; }
}