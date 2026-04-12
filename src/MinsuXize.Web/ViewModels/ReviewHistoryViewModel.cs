using MinsuXize.Web.Models;

namespace MinsuXize.Web.ViewModels;

public sealed class ReviewHistoryViewModel
{
    public required SubmissionRecord Submission { get; init; }
    public required IReadOnlyList<ReviewHistory> History { get; init; }
}