using SubscriptionManager.Domain.Entities;
using SubscriptionManager.Domain.Exceptions;

namespace SubscriptionManager.Domain.Tests;

public class SubscriptionTests
{
    [Fact]
    public void Should_Create_Subscription_With_Valid_Parameters()
    {
        // Arrange
        var name = "Test Subscription";
        var dateFrom = DateTime.Today;
        var dateTo = DateTime.Today.AddMonths(1);
        decimal price = 10.99m;
        var avatarPath = "path/to/avatar.png";

        // Act
        var subscription = new Subscription(name, dateFrom, dateTo, price, avatarPath);

        // Assert
        Assert.NotEqual(Guid.Empty, subscription.Id);
        Assert.Equal(name, subscription.Name);
        Assert.Equal(dateFrom, subscription.DateFrom);
        Assert.Equal(dateTo, subscription.DateTo);
        Assert.Equal(price, subscription.Price);
        Assert.Equal(avatarPath, subscription.AvatarPath);
    }

    [Fact]
    public void Should_Throw_Exception_When_DateFrom_Is_Greater_Than_DateTo()
    {
        // Arrange
        var name = "Invalid Subscription";
        var dateFrom = DateTime.Today.AddMonths(1);
        var dateTo = DateTime.Today;
        decimal price = 10.99m;
        var avatarPath = "avatar.png";

        // Act & Assert
        Assert.Throws<SubscriptionDomainException>(
            () => new Subscription(name, dateFrom, dateTo, price, avatarPath)
        );
    }

    [Fact]
    public void Should_Update_Subscription_Correctly()
    {
        // Arrange
        var subscription = new Subscription("Initial Name", DateTime.Today, DateTime.Today.AddMonths(1), 15m, "initial.png");
        var newName = "Updated Name";
        var newDateFrom = DateTime.Today.AddDays(1);
        var newDateTo = DateTime.Today.AddMonths(2);
        decimal newPrice = 20m;
        var newAvatarPath = "updated.png";

        // Act
        subscription.UpdateSubscription(newName, newDateFrom, newDateTo, newPrice, newAvatarPath);

        // Assert
        Assert.Equal(newName, subscription.Name);
        Assert.Equal(newDateFrom, subscription.DateFrom);
        Assert.Equal(newDateTo, subscription.DateTo);
        Assert.Equal(newPrice, subscription.Price);
        Assert.Equal(newAvatarPath, subscription.AvatarPath);
    }
}