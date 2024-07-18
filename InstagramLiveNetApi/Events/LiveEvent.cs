using Newtonsoft.Json;

namespace InstagramLiveNetApi.Events;

public class LiveEvent
{
    public string ToJson()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}