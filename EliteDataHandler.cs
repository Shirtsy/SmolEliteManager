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

    private static List<JsonDocument> ReadJson(string filePath)
    {
        try
        {
            using var stream = File.OpenRead(filePath);
            var doc = JsonDocument.Parse(stream);
            return [doc];
        }
        catch (Exception ex) when (ex is IOException or JsonException)
        {
            Console.WriteLine($"Error reading or parsing file: {ex.Message}");
        }

        return [];
    }

    private static List<JsonDocument> ReadJsonLines(string filePath)
    {
        var results = new List<JsonDocument>();

        try
        {
            using var reader = new StreamReader(filePath);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                try
                {
                    var doc = JsonDocument.Parse(line);
                    results.Add(doc);
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Error parsing JSON line: {ex.Message}");
                }
            }
        }
        catch (Exception ex) when (ex is IOException)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
        }

        return results;
    }

}
