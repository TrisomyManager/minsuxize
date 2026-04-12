using MinsuXize.Web.Models;

namespace MinsuXize.Web.ViewModels;

public sealed class RegionDetailsViewModel
{
    public required Region Region { get; init; }
    public Region? ParentRegion { get; init; }
    public required IReadOnlyList<Region> ChildRegions { get; init; }
    public required IReadOnlyList<FolkloreEntry> Entries { get; init; }
    public required int DescendantRegionCount { get; init; }
}
