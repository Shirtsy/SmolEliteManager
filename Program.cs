var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/weatherforecast", () =>
{
    var forecast =
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now),
            Random.Shared.Next(-20, 55),
            "Text"
        );
    return forecast;
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
