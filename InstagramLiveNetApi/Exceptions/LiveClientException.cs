using System.Runtime.Serialization;

namespace InstagramLiveNetApi.Exceptions;

public class LiveClientException : Exception
{
    public LiveClientException()
    {
    }

    protected LiveClientException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public LiveClientException(string? message) : base(message)
    {
    }

    public LiveClientException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
    
}