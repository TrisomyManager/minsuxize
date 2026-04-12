namespace MinsuXize.Web.Models;

public sealed class Festival
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string Category { get; init; }
    public required string LunarLabel { get; init; }
    public required string Summary { get; init; }
    public IReadOnlyList<string> CoreTopics { get; init; } = [];
}
