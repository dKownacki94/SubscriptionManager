namespace SubscriptionManager.Domain.Exceptions;

/// <summary>
/// Wyjątek specyficzny dla logiki domenowej subskrypcji.
/// </summary>
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