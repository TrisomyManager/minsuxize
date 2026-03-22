using MinsuXize.Web.Models;

namespace MinsuXize.Web.ViewModels;

public sealed class AdminDashboardViewModel
{
    public required IReadOnlyList<SubmissionRecord> Submissions { get; init; }
    public required IReadOnlyDictionary<int, Region> RegionsById { get; init; }
    public required IReadOnlyDictionary<int, Festival> FestivalsById { get; init; }
}
