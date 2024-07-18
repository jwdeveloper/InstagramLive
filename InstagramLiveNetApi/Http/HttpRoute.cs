using InstagramLiveNetApi.Exceptions;
using Newtonsoft.Json;

namespace InstagramLiveNetApi.Http;

public abstract class HttpRoute<TInput, TOutput>
{
    public abstract Task<TOutput> Execute(TInput input);

    protected async Task<HttpResponseMessage> CheckLogin(HttpResponseMessage response)
    {
        var responseData = await response.Content.ReadAsStringAsync();

        if (!response.Content.Headers.ContentType.MediaType.Equals("text/html"))
        {
            return response;
        }

        if (responseData.Contains("<title>Login"))
        {
            throw new LiveClientHttpException(
                "User is not authenticated or authentication expired. Please login first.");
        }

        if (response.Headers.Contains("location") &&
            response.Headers.Location?.ToString().Contains("/challenge/") == true)
        {
            throw new LiveClientHttpException("User was hit with a challenge: " + response.Headers.Location);
        }

        return response;
    }

    protected async Task<T> ToJson<T>(HttpResponseMessage httpRequestMessage)
    {
        var responseData = await httpRequestMessage.Content.ReadAsStringAsync();
        var respone = JsonConvert.DeserializeObject<T>(responseData);
        return respone;
    }
}