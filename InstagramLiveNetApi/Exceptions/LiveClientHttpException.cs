using System.Runtime.Serialization;

namespace InstagramLiveNetApi.Exceptions;

public class LiveClientHttpException : LiveClientException
{
    public LiveClientHttpException()
    {
    }

    protected LiveClientHttpException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public LiveClientHttpException(string? message) : base(message)
    {
    }

    public LiveClientHttpException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}