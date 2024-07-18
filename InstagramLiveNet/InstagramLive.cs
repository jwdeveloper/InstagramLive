using InstagramLiveNetApi;

namespace InstagramLiveNet;

/// <summary>
/// Provides a factory method for creating new instances of <see cref="ILiveConfigurationBuilder"/>.
/// </summary>
public class InstagramLive
{
    /// <summary>
    /// Creates a new instance of <see cref="ILiveConfigurationBuilder"/>.
    /// </summary>
    /// <returns>A new instance of <see cref="ILiveConfigurationBuilder"/>.</returns>
    public static ILiveConfigurationBuilder NewClient()
    {
        return new LiveClientBuilder();
    }
}