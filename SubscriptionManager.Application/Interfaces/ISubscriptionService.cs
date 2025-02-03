using SubscriptionManager.Application.DTOs;

namespace SubscriptionManager.Application.Interfaces;

/// <summary>
/// Interfejs definiujący operacje związane z subskrypcjami.
/// </summary>
public interface ISubscriptionService
{
    /// <summary>
    /// Pobiera wszystkie subskrypcje.
    /// </summary>
    /// <returns>Lista subskrypcji w postaci DTO.</returns>
    Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync();

    /// <summary>
    /// Pobiera subskrypcję o podanym identyfikatorze.
    /// </summary>
    /// <param name="id">Identyfikator subskrypcji.</param>
    /// <returns>Obiekt DTO reprezentujący subskrypcję lub null, jeśli nie znaleziono.</returns>
    Task<SubscriptionDto> GetSubscriptionByIdAsync(Guid id);

    /// <summary>
    /// Dodaje nową subskrypcję.
    /// </summary>
    /// <param name="subscriptionDto">Obiekt DTO z danymi subskrypcji.</param>
    Task AddSubscriptionAsync(SubscriptionDto subscriptionDto);

    /// <summary>
    /// Aktualizuje istniejącą subskrypcję.
    /// </summary>
    /// <param name="subscriptionDto">Obiekt DTO z nowymi danymi subskrypcji.</param>
    Task UpdateSubscriptionAsync(SubscriptionDto subscriptionDto);

    /// <summary>
    /// Usuwa subskrypcję o podanym identyfikatorze.
    /// </summary>
    /// <param name="id">Identyfikator subskrypcji do usunięcia.</param>
    Task DeleteSubscriptionAsync(Guid id);
}