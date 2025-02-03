using Moq;
using SubscriptionManager.Application.DTOs;
using SubscriptionManager.Application.Services;
using SubscriptionManager.Domain.Entities;
using SubscriptionManager.Domain.Interfaces;

namespace SubscriptionManager.Application.Tests;

public class SubscriptionServiceTests
{
    [Fact]
    public async Task AddSubscription_Should_Add_Subscription_To_Repository()
    {
        // Arrange
        var subscriptionList = new List<Subscription>();
        var repositoryMock = new Mock<ISubscriptionRepository>();

        // Konfiguracja metody AddAsync: dodajemy encję do listy
        repositoryMock.Setup(r => r.AddAsync(It.IsAny<Subscription>()))
            .Returns((Subscription s) =>
            {
                subscriptionList.Add(s);
                return Task.CompletedTask;
            });

        // Konfiguracja metody GetAllAsync: zwracamy stan listy
        repositoryMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(() => subscriptionList);

        var subscriptionService = new SubscriptionService(repositoryMock.Object);
        var subscriptionDto = new SubscriptionDto
        {
            Name = "Test Subscription",
            DateFrom = DateTime.Today,
            DateTo = DateTime.Today.AddMonths(1),
            Price = 20.0m,
            AvatarPath = "avatar.png"
        };

        // Act
        await subscriptionService.AddSubscriptionAsync(subscriptionDto);

        // Assert
        Assert.Single(subscriptionList);
        var added = subscriptionList.First();
        Assert.Equal(subscriptionDto.Name, added.Name);

        // Dodatkowa weryfikacja wywołania metody AddAsync dokładnie raz
        repositoryMock.Verify(r => r.AddAsync(It.IsAny<Subscription>()), Times.Once);
    }

    [Fact]
    public async Task UpdateSubscription_Should_Update_Existing_Subscription()
    {
        // Arrange
        var subscriptionList = new List<Subscription>();
        var repositoryMock = new Mock<ISubscriptionRepository>();

        // Konfiguracja: metoda AddAsync – zapisujemy do listy
        repositoryMock.Setup(r => r.AddAsync(It.IsAny<Subscription>()))
            .Returns((Subscription s) =>
            {
                subscriptionList.Add(s);
                return Task.CompletedTask;
            });

        // Konfiguracja: metoda GetAllAsync – zwracamy stan listy
        repositoryMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(() => subscriptionList);

        // Konfiguracja: metoda GetByIdAsync – wyszukujemy w liście po identyfikatorze
        repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Guid id) => subscriptionList.FirstOrDefault(s => s.Id == id));

        // Konfiguracja: metoda UpdateAsync – aktualizujemy element w liście
        repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Subscription>()))
            .Returns((Subscription s) =>
            {
                var index = subscriptionList.FindIndex(x => x.Id == s.Id);
                if (index != -1)
                    subscriptionList[index] = s;
                return Task.CompletedTask;
            });

        var subscriptionService = new SubscriptionService(repositoryMock.Object);

        // Najpierw dodajemy subskrypcję
        var subscriptionDto = new SubscriptionDto
        {
            Name = "Initial Subscription",
            DateFrom = DateTime.Today,
            DateTo = DateTime.Today.AddMonths(1),
            Price = 15.0m,
            AvatarPath = "initial.png"
        };
        await subscriptionService.AddSubscriptionAsync(subscriptionDto);

        // Pobieramy dodaną subskrypcję (przyjmujemy, że lista zawiera tylko jedną)
        var addedSubscription = subscriptionList.First();

        // Przygotowujemy dane do aktualizacji – Id musi być zgodne z istniejącą subskrypcją
        var updatedDto = new SubscriptionDto
        {
            Id = addedSubscription.Id,
            Name = "Updated Subscription",
            DateFrom = DateTime.Today.AddDays(1),
            DateTo = DateTime.Today.AddMonths(2),
            Price = 25.0m,
            AvatarPath = "updated.png"
        };

        // Act
        await subscriptionService.UpdateSubscriptionAsync(updatedDto);

        // Assert
        var updatedSubscription = subscriptionList.FirstOrDefault(s => s.Id == addedSubscription.Id);
        Assert.NotNull(updatedSubscription);
        Assert.Equal(updatedDto.Name, updatedSubscription.Name);
        Assert.Equal(updatedDto.DateFrom, updatedSubscription.DateFrom);
        Assert.Equal(updatedDto.DateTo, updatedSubscription.DateTo);
        Assert.Equal(updatedDto.Price, updatedSubscription.Price);
        Assert.Equal(updatedDto.AvatarPath, updatedSubscription.AvatarPath);

        // Weryfikacja, że UpdateAsync zostało wywołane dokładnie raz
        repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Subscription>()), Times.Once);
    }
}