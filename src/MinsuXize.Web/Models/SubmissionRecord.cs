using System.Text.Json;

namespace MinsuXize.Web.Models;

public sealed class SubmissionRecord
{
    public required int Id { get; init; }
    public required string ContributorName { get; init; }
    public required int RegionId { get; init; }
    public required int FestivalId { get; init; }
    public required string Title { get; init; }
    public required string Summary { get; init; }
    public required string SourceSummary { get; init; }
    public string? Contact { get; init; }
    public required DateTime SubmittedAt { get; init; }
    public SubmissionStatus Status { get; set; }
    public string? ReviewerNote { get; set; }
    
    // 新增字段
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public int Version { get; set; } = 1;
    public string? ChangeLog { get; set; }
    public List<string> Images { get; set; } = new();
    public List<string> Videos { get; set; } = new();
    public List<string> Audios { get; set; } = new();
    public LocationInfo? Location { get; set; }
}

public class LocationInfo
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Province { get; set; }
    public string? Country { get; set; }
    public string? Description { get; set; }
}
