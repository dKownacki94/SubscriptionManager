using SubscriptionManager.Application.DTOs;
using SubscriptionManager.Application.Interfaces;
using SubscriptionManager.Application.Services;
using SubscriptionManager.Domain.Interfaces;
using SubscriptionManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services
builder.Services.AddSingleton<ISubscriptionRepository, InMemorySubscriptionRepository>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Define API endpoints
app.MapGet("/subscriptions", async (ISubscriptionService service) =>
{
    return Results.Ok(await service.GetAllSubscriptionsAsync());
});

app.MapGet("/subscriptions/{id}", async (ISubscriptionService service, Guid id) =>
{
    var subscription = await service.GetSubscriptionByIdAsync(id);
    return subscription is not null ? Results.Ok(subscription) : Results.NotFound();
});

app.MapPost("/subscriptions", async (ISubscriptionService service, SubscriptionDto dto) =>
{
    await service.AddSubscriptionAsync(dto);
    return Results.Created($"/subscriptions/{dto.Id}", dto);
});

app.MapPut("/subscriptions/{id}", async (ISubscriptionService service, Guid id, SubscriptionDto dto) =>
{
    var existingSubscription = await service.GetSubscriptionByIdAsync(id);
    if (existingSubscription is null) return Results.NotFound();

    await service.UpdateSubscriptionAsync(dto);
    return Results.NoContent();
});

app.MapDelete("/subscriptions/{id}", async (ISubscriptionService service, Guid id) =>
{
    var existingSubscription = await service.GetSubscriptionByIdAsync(id);
    if (existingSubscription is null) return Results.NotFound();

    await service.DeleteSubscriptionAsync(id);
    return Results.NoContent();
});

app.Run();