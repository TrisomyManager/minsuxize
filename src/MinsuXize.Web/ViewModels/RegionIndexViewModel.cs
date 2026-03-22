using MinsuXize.Web.Models;

namespace MinsuXize.Web.ViewModels;

public sealed class RegionIndexViewModel
{
    public string? Keyword { get; init; }
    public required int RegionCount { get; init; }
    public required IReadOnlyList<Region> Regions { get; init; }
}
