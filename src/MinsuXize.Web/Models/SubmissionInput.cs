namespace MinsuXize.Web.Models;

public sealed class SubmissionInput
{
    public required string ContributorName { get; init; }
    public required int RegionId { get; init; }
    public required int FestivalId { get; init; }
    public required string Title { get; init; }
    public required string Summary { get; init; }
    public required string SourceSummary { get; init; }
    public string? Contact { get; init; }
}
