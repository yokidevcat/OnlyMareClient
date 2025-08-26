namespace OnlyMare.WebAPI.SignalR;

public class OnlyMareAuthFailureException : Exception
{
    public OnlyMareAuthFailureException(string reason)
    {
        Reason = reason;
    }

    public string Reason { get; }
}