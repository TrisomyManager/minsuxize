using Microsoft.AspNetCore.Mvc.Rendering;
using MinsuXize.Web.Models;

namespace MinsuXize.Web.ViewModels;

public sealed class EntrySearchViewModel
{
    public string? Keyword { get; init; }
    public int? RegionId { get; init; }
    public int? FestivalId { get; init; }
    public required IReadOnlyList<FolkloreEntry> Entries { get; init; }
    public required IReadOnlyDictionary<int, Region> RegionsById { get; init; }
    public required IReadOnlyDictionary<int, Festival> FestivalsById { get; init; }
    public required IReadOnlyList<SelectListItem> RegionOptions { get; init; }
    public required IReadOnlyList<SelectListItem> FestivalOptions { get; init; }
    public string? SelectedRegionLabel { get; init; }
}
