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
    
    // 新增字段
    public List<string> Images { get; init; } = new();
    public List<string> Videos { get; init; } = new();
    public List<string> Audios { get; init; } = new();
    public LocationInfo? Location { get; init; }
    public string? ChangeLog { get; init; }
}
