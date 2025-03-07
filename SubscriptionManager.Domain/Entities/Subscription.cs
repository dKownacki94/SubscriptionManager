using SubscriptionManager.Domain.Exceptions;

namespace SubscriptionManager.Domain.Entities;

public class Subscription
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public DateTime DateFrom { get; private set; }
    public DateTime DateTo { get; private set; }
    public decimal Price { get; private set; }
    public string AvatarPath { get; private set; }

    public Subscription(string name, DateTime dateFrom, DateTime dateTo, decimal price, string avatarPath)
    {
        ValidateParameters(name, dateFrom, dateTo, price);

        Id = Guid.NewGuid();
        Name = name;
        DateFrom = dateFrom;
        DateTo = dateTo;
        Price = price;
        AvatarPath = avatarPath;
    }

    public void UpdateSubscription(string name, DateTime dateFrom, DateTime dateTo, decimal price, string avatarPath)
    {
        ValidateParameters(name, dateFrom, dateTo, price);

        Name = name;
        DateFrom = dateFrom;
        DateTo = dateTo;
        Price = price;
        AvatarPath = avatarPath;
    }

    private void ValidateParameters(string name, DateTime dateFrom, DateTime dateTo, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nazwa subskrypcji nie może być pusta.", nameof(name));

        if (dateFrom > dateTo)
            throw new SubscriptionDomainException("Data rozpoczęcia nie może być późniejsza niż data zakończenia.");

        if (price < 0)
            throw new ArgumentException("Cena nie może być ujemna.", nameof(price));
    }

    public SubscriptionStatus GetStatus(DateTime currentDate)
    {
        if (DateTo < currentDate)
        {
            return SubscriptionStatus.Inactive;
        }

        var daysRemaining = (DateTo - currentDate).Days;

        if (daysRemaining <= 3)
        {
            return SubscriptionStatus.Expiring;
        }

        return SubscriptionStatus.Active;
    }
}