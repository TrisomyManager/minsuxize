using System.Text.Json;

namespace MinsuXize.Web.Data.Seed;

internal static class JsonListSerializer
{
    public static string Serialize(params string[] items) =>
        JsonSerializer.Serialize(items);

    public static IReadOnlyList<string> Deserialize(string? json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return [];
        }

        return JsonSerializer.Deserialize<List<string>>(json) ?? [];
    }
}
