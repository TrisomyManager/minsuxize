namespace MinsuXize.Web.Models;

public class ReviewHistory
{
    public int Id { get; set; }
    public int SubmissionId { get; set; }
    public SubmissionStatus OldStatus { get; set; }
    public SubmissionStatus NewStatus { get; set; }
    public string Reviewer { get; set; } = string.Empty;
    public string? ReviewerNote { get; set; }
    public DateTime ReviewedAt { get; set; }
    public string? ChangeSummary { get; set; }
    public string? MetadataJson { get; set; }
}