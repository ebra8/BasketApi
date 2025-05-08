using BasketApi.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// In-memory basket storage: userId -> list of BasketItems
var _baskets = new Dictionary<int, List<BasketItem>>();

// Add item to basket
app.MapPost("/api/basket/add", (int userId, BasketItem item) =>
{
    if (!_baskets.ContainsKey(userId))
        _baskets[userId] = new List<BasketItem>();
    var basket = _baskets[userId];
    var existing = basket.FirstOrDefault(x => x.PackageId == item.PackageId);
    if (existing != null)
        existing.Quantity += item.Quantity;
    else
        basket.Add(item);
    return Results.Ok(new { message = "Item added to basket." });
});

// Remove item from basket
app.MapPost("/api/basket/remove", (int userId, int packageId) =>
{
    if (!_baskets.ContainsKey(userId))
        return Results.NotFound(new { message = "Basket not found." });
    var basket = _baskets[userId];
    var item = basket.FirstOrDefault(x => x.PackageId == packageId);
    if (item == null)
        return Results.NotFound(new { message = "Item not found in basket." });
    basket.Remove(item);
    return Results.Ok(new { message = "Item removed from basket." });
});

// View basket
app.MapGet("/api/basket", (int userId) =>
{
    if (!_baskets.ContainsKey(userId))
        return Results.Ok(new List<BasketItem>());
    return Results.Ok(_baskets[userId]);
});

// Checkout
app.MapPost("/api/basket/checkout", (int userId) =>
{
    if (_baskets.ContainsKey(userId))
        _baskets[userId].Clear();
    return Results.Ok(new { message = "Booking Confirmed!" });
});

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
