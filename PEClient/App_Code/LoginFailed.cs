using System;
using System.Runtime.Serialization;

[Serializable]
public class LoginFailed : Exception
{
    public LoginFailed()
    {
    }

    public LoginFailed(int failID)
    {
    }

    public LoginFailed(string message) : base(message)
    {
    }

    public LoginFailed(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected LoginFailed(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}