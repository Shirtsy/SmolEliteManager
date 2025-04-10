var app = WebApplication.Create();
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles(new StaticFileOptions { ServeUnknownFileTypes = true });

// Could use images with alpha channel and compare average similarity per-pixel to detect
// key features on screen to determine which menu it's on

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