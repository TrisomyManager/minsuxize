using Microsoft.AspNetCore.Mvc.Rendering;
using MinsuXize.Web.Models;

namespace MinsuXize.Web.ViewModels;

public sealed class HomePageViewModel
{
    public required int RegionCount { get; init; }
    public required int FestivalCount { get; init; }
    public required int EntryCount { get; init; }
    public required int PendingSubmissionCount { get; init; }
    public required IReadOnlyList<Region> FeaturedRegions { get; init; }
    public required IReadOnlyList<Festival> FeaturedFestivals { get; init; }
    public required IReadOnlyList<FolkloreEntry> FeaturedEntries { get; init; }
    public required IReadOnlyList<SelectListItem> RegionOptions { get; init; }
    public required IReadOnlyList<SelectListItem> FestivalOptions { get; init; }
}
