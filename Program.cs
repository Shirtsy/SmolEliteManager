var app = WebApplication.Create();
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles(new StaticFileOptions { ServeUnknownFileTypes = true });

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
