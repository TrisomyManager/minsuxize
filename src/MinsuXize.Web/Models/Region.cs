namespace MinsuXize.Web.Models;

public sealed class Region
{
    public required int Id { get; init; }
    public required string Slug { get; init; }
    public required string Name { get; init; }
    public required string Type { get; init; }
    public int? ParentId { get; init; }
    public required string FullPath { get; init; }
    public required string Summary { get; init; }
    public required string CulturalFocus { get; init; }
    public IReadOnlyList<string> Highlights { get; init; } = [];
    public required DateTime UpdatedAt { get; init; }
}
