namespace MinsuXize.Web.Models;

public sealed class StructuredKnowledgeEntry
{
    public required string Title { get; init; }
    public required string Slug { get; init; }
    public required string ContentType { get; init; }
    public required string Summary { get; init; }
    public required Region Region { get; init; }
    public required Festival Festival { get; init; }
    public IReadOnlyDictionary<string, string> RegionFields { get; init; } = new Dictionary<string, string>();
    public IReadOnlyList<string> Materials { get; init; } = [];
    public IReadOnlyList<string> Preparations { get; init; } = [];
    public IReadOnlyList<string> Steps { get; init; } = [];
    public IReadOnlyList<string> Taboos { get; init; } = [];
    public IReadOnlyList<string> RegionalDifferences { get; init; } = [];
    public IReadOnlyList<EntryFaq> FAQ { get; init; } = [];
    public IReadOnlyList<SourceEvidence> Sources { get; init; } = [];
    public required string ReviewStatus { get; init; }
    public required string ConfidenceLevel { get; init; }
    public required DateTime UpdatedAt { get; init; }
}
