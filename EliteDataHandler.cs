using System.Text.Json;

public static class EliteDataHandler
{
    public static IEnumerable<JsonDocument> MarketData =>
        ReadJson(Path.Combine(EliteFolderPath, "Market.json"));

    public static IEnumerable<JsonDocument> OutfittingData =>
        ReadJson(Path.Combine(EliteFolderPath, "Outfitting.json"));

    public static IEnumerable<JsonDocument> ShipyardData =>
        ReadJson(Path.Combine(EliteFolderPath, "Shipyard.json"));

    public static IEnumerable<JsonDocument> StatusData =>
        ReadJson(Path.Combine(EliteFolderPath, "Status.json"));

    public static IEnumerable<JsonDocument> JournalData =>
        ReadJsonLines(Directory.GetFiles(EliteFolderPath, "Journal.*.log")
            .OrderByDescending(file => File.GetLastWriteTime(file))
            .First());

    public static string SerializeDocuments(IEnumerable<JsonDocument> documents) =>
        JsonSerializer.Serialize(
            documents.Select(doc => doc.RootElement).ToArray()
        );

    private static string EliteFolderPath =>
    Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        "Saved Games",
        "Frontier Developments",
        "Elite Dangerous"
    );

    private static IEnumerable<JsonDocument> ReadJson(string filePath)
    {
        if (!File.Exists(filePath)) yield break;
        yield return JsonDocument.Parse(File.ReadAllText(filePath));
    }

    private static IEnumerable<JsonDocument> ReadJsonLines(string filePath)
    {
        if (!File.Exists(filePath)) yield break;

        using var reader = new StreamReader(filePath);
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                yield return JsonDocument.Parse(line);
            }
        }
    }
}
