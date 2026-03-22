namespace MinsuXize.Web.Models;

public sealed class SourceEvidence
{
    public required int Id { get; init; }
    public required string SourceType { get; init; }
    public required string Title { get; init; }
    public required string Contributor { get; init; }
    public required string RecordedAt { get; init; }
    public required string Citation { get; init; }
    public string? Url { get; init; }
    public string? Notes { get; init; }
}
