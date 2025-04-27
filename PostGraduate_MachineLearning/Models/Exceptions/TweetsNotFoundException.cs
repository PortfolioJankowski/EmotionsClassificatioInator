using System.Runtime.Serialization;

namespace Models.Exceptions;

public class TweetsNotFoundException : Exception
{
    public TweetsNotFoundException() : base("No tweets found matching the specified criteria") { }
 
    public TweetsNotFoundException(string? message) : base(message)
    {

    }

    public TweetsNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

}

