using System.Net;
using InstagramLiveNet.Http.Routes;
using Newtonsoft.Json;

namespace InstagramLiveNet.Http;

public class LiveHttpClient
{
    private static readonly string COOKIE_DOMAIN = "instagram.com";
    private static readonly string ORIGIN = "https://www.instagram.com/";

    private static readonly Dictionary<string, string> DEFAULT_HEADERS = new Dictionary<string, string>
    {
        { "Accept", "*/*" },
        { "Accept-Encoding", "gzip, deflate" },
        { "Accept-Language", "en-CA,en;q=0.9" },
        { "Cache-Control", "no-cache" },
        { "Connection", "keep-alive" },
        { "Dpr", "3" },
        { "Host", "www.instagram.com" },
        { "Pragma", "no-cache" },
        { "Sec-Ch-Prefers-Color-Scheme", "dark" },
        { "Sec-Ch-Ua", "\"Not/A)Brand\";v=\"8\", \"Chromium\";v=\"126\", \"Google Chrome\";v=\"126\"" },
        {
            "Sec-Ch-Ua-Full-Version-List",
            "\"Not/A)Brand\";v=\"8.0.0.0\", \"Chromium\";v=\"126.0.6478.127\", \"Google Chrome\";v=\"126.0.6478.127\""
        },
        { "Sec-Ch-Ua-Mobile", "?0" },
        { "Sec-Ch-Ua-Model", "" },
        { "X-IG-App-ID", "936619743392459" },
        { "Sec-Ch-Ua-Platform", "\"macOS\"" },
        { "Sec-Ch-Ua-Platform-Version", "\"14.4.0\"" },
        { "Sec-Fetch-Dest", "document" },
        { "Sec-Fetch-Mode", "navigate" },
        { "Sec-Fetch-Site", "none" },
        { "Sec-Fetch-User", "?1" },
        { "Upgrade-Insecure-Requests", "1" },
        {
            "User-Agent",
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36"
        },
        { "Viewport-Width", "499" },
        { "Origin", ORIGIN }
    };

    private readonly HttpClient httpClient;
    private readonly CookieContainer cookieContainer;

    public LiveHttpClient(Uri proxy = null)
    {
        cookieContainer = new CookieContainer();
        var handler = new HttpClientHandler
        {
            CookieContainer = cookieContainer,
            UseCookies = true,
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        };

        if (proxy != null)
        {
            handler.Proxy = new WebProxy(proxy);
        }

        this.httpClient = new HttpClient(handler);
        AddDefaultHeaders();
    }

    private void AddDefaultHeaders()
    {
        foreach (var header in DEFAULT_HEADERS)
        {
            this.httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
        }
    }

    public async Task<HttpResponseMessage> GetAsync(string url)
    {
        var response = await this.httpClient.GetAsync(url);
        return response;
    }

    public async Task<T> GetAsync<T>(string url)
    {
        var response = await GetAsync(url);
        var responseData = await response.Content.ReadAsStringAsync();
        var objectResponse = JsonConvert.DeserializeObject<T>(responseData);
        return objectResponse;
    }

    public void SetCookie(string name, string value)
    {
        var cookie = new Cookie(name, value, "/", COOKIE_DOMAIN)
        {
            Secure = true
        };
        this.cookieContainer.Add(new Uri(ORIGIN), cookie);
    }

    public void SetDefaultRequestHeader(string name, string value)
    {
        httpClient.DefaultRequestHeaders.Add(name, value);
    }


    public string GetCookie(string key)
    {
        var cookies = cookieContainer.GetCookies(new Uri("https://www.instagram.com"));
        foreach (Cookie cookie in cookies)
        {
            if (cookie.Name == key)
            {
                return cookie.Value;
            }
        }

        return null;
    }

    public static string GenerateDeviceId()
    {
        return Guid.NewGuid().ToString().ToUpper();
    }

    public static Task SleepRandom(int min = 1, int max = 5)
    {
        var rnd = new Random();
        int delay = rnd.Next(min, max + 1) * 1000;
        return Task.Delay(delay);
    }

    public async Task<HttpResponseMessage> PostAsync(string url, FormUrlEncodedContent requestContent)
    {
        var response = await this.httpClient.PostAsync(url, requestContent);
        return response;
    }
}