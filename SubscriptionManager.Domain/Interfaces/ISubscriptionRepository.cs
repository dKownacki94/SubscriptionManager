using SubscriptionManager.Domain.Entities;

namespace SubscriptionManager.Domain.Interfaces;

/// <summary>
/// Interfejs repozytorium umożliwiający operacje CRUD na subskrypcjach.
/// </summary>
public interface ISubscriptionRepository
{
    /// <summary>
    /// Pobiera wszystkie subskrypcje.
    /// </summary>
    /// <returns>Lista subskrypcji.</returns>
    Task<IEnumerable<Subscription>> GetAllAsync();

    /// <summary>
    /// Pobiera subskrypcję na podstawie identyfikatora.
    /// </summary>
    /// <param name="id">Identyfikator subskrypcji.</param>
    /// <returns>Subskrypcja lub null, jeśli nie została znaleziona.</returns>
    Task<Subscription> GetByIdAsync(Guid id);

    /// <summary>
    /// Dodaje nową subskrypcję.
    /// </summary>
    /// <param name="subscription">Obiekt subskrypcji do dodania.</param>
    Task AddAsync(Subscription subscription);

    /// <summary>
    /// Aktualizuje istniejącą subskrypcję.
    /// </summary>
    /// <param name="subscription">Obiekt subskrypcji do aktualizacji.</param>
    Task UpdateAsync(Subscription subscription);

    /// <summary>
    /// Usuwa subskrypcję o podanym identyfikatorze.
    /// </summary>
    /// <param name="id">Identyfikator subskrypcji do usunięcia.</param>
    Task DeleteAsync(Guid id);
}