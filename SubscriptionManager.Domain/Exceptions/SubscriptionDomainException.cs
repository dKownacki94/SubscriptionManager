namespace SubscriptionManager.Domain.Exceptions;

public class SubscriptionDomainException : Exception
{
    public SubscriptionDomainException() { }

    public SubscriptionDomainException(string message)
        : base(message)
    {
    }

    public SubscriptionDomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}