using SubscriptionManager.Domain.Exceptions;

namespace SubscriptionManager.Domain.Entities;

/// <summary>
/// Reprezentuje subskrypcję w systemie.
/// </summary>
public class Subscription
{
    /// <summary>
    /// Unikalny identyfikator subskrypcji.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Nazwa subskrypcji.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Data rozpoczęcia subskrypcji.
    /// </summary>
    public DateTime DateFrom { get; private set; }

    /// <summary>
    /// Data zakończenia subskrypcji.
    /// </summary>
    public DateTime DateTo { get; private set; }

    /// <summary>
    /// Cena subskrypcji.
    /// </summary>
    public decimal Price { get; private set; }

    /// <summary>
    /// Ścieżka do awatara powiązanego z subskrypcją.
    /// </summary>
    public string AvatarPath { get; private set; }

    /// <summary>
    /// Konstruktor tworzący nową subskrypcję.
    /// </summary>
    /// <param name="name">Nazwa subskrypcji.</param>
    /// <param name="dateFrom">Data rozpoczęcia.</param>
    /// <param name="dateTo">Data zakończenia.</param>
    /// <param name="price">Cena subskrypcji.</param>
    /// <param name="avatarPath">Ścieżka do awatara.</param>
    /// <exception cref="ArgumentException">Wyrzucany gdy którykolwiek z warunków walidacji nie jest spełniony.</exception>
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

    /// <summary>
    /// Aktualizuje dane subskrypcji.
    /// </summary>
    /// <param name="name">Nowa nazwa subskrypcji.</param>
    /// <param name="dateFrom">Nowa data rozpoczęcia.</param>
    /// <param name="dateTo">Nowa data zakończenia.</param>
    /// <param name="price">Nowa cena subskrypcji.</param>
    /// <param name="avatarPath">Nowa ścieżka do awatara.</param>
    public void UpdateSubscription(string name, DateTime dateFrom, DateTime dateTo, decimal price, string avatarPath)
    {
        ValidateParameters(name, dateFrom, dateTo, price);

        Name = name;
        DateFrom = dateFrom;
        DateTo = dateTo;
        Price = price;
        AvatarPath = avatarPath;
    }

    /// <summary>
    /// Metoda walidująca podstawowe parametry subskrypcji.
    /// </summary>
    private void ValidateParameters(string name, DateTime dateFrom, DateTime dateTo, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nazwa subskrypcji nie może być pusta.", nameof(name));

        if (dateFrom > dateTo)
            throw new SubscriptionDomainException("Data rozpoczęcia nie może być późniejsza niż data zakończenia.");

        if (price < 0)
            throw new ArgumentException("Cena nie może być ujemna.", nameof(price));
    }
}