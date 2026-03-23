using MinsuXize.Web.Models;

namespace MinsuXize.Web.ViewModels;

public sealed class BulkReviewViewModel
{
    public List<int> SubmissionIds { get; set; } = new();
    public SubmissionStatus Status { get; set; }
    public string? ReviewerNote { get; set; }
}