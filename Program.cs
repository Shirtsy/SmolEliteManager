var app = WebApplication.Create();
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles(new StaticFileOptions { ServeUnknownFileTypes = true });

app.MapGet("market", () =>
    EliteDataHandler.SerializeDocuments(EliteDataHandler.MarketData)
);

app.MapGet("outfitting", () =>
    EliteDataHandler.SerializeDocuments(EliteDataHandler.OutfittingData)
);

app.MapGet("shipyard", () =>
    EliteDataHandler.SerializeDocuments(EliteDataHandler.ShipyardData)
);

app.MapGet("status", () =>
    EliteDataHandler.SerializeDocuments(EliteDataHandler.StatusData)
);

app.MapGet("journal", () =>
    EliteDataHandler.SerializeDocuments(EliteDataHandler.JournalData)
);

app.Run();