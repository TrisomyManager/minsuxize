namespace MinsuXize.Web.Data.Entities;

public sealed class SourceEvidenceEntity
{
    public int Id { get; set; }
    public string SourceType { get; set; } = string.Empty;
    public string SourceLevel { get; set; } = "unverified";
    public string Title { get; set; } = string.Empty;
    public string Contributor { get; set; } = string.Empty;
    public string RecordedAt { get; set; } = string.Empty;
    public string Citation { get; set; } = string.Empty;
    public string? Url { get; set; }
    public string? Notes { get; set; }
}
