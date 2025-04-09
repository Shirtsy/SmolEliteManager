var app = WebApplication.Create();
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles(new StaticFileOptions { ServeUnknownFileTypes = true });

app.MapGet("/weatherforecast", () =>
    new WeatherForecast(
        DateOnly.FromDateTime(DateTime.Now),
        Random.Shared.Next(-20, 55),
        "Text"
    )
);

app.MapGet("market", () =>
    EliteDataHandler.SerializeDocuments(EliteDataHandler.MarketData)
);

app.MapGet("outfitting", () =>
    EliteDataHandler.SerializeDocuments(EliteDataHandler.OutfittingData)
);

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}