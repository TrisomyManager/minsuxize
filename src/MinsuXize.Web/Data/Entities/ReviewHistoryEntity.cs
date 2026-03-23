namespace MinsuXize.Web.Data.Entities;

public class ReviewHistoryEntity
{
    public int Id { get; set; }
    public int SubmissionId { get; set; }
    public int OldStatus { get; set; }
    public int NewStatus { get; set; }
    public string Reviewer { get; set; } = string.Empty;
    public string? ReviewerNote { get; set; }
    public DateTime ReviewedAt { get; set; }
    public string? ChangeSummary { get; set; }
    public string? MetadataJson { get; set; }
}