using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MinsuXize.Web.Models;
using MinsuXize.Web.Services;
using MinsuXize.Web.ViewModels;

namespace MinsuXize.Web.Controllers;

public sealed class EntriesController : Controller
{
    private readonly IFolkloreRepository _repository;

    public EntriesController(IFolkloreRepository repository)
    {
        _repository = repository;
    }

    public IActionResult Index(string? keyword, int? regionId, int? festivalId)
    {
        var regions = _repository.GetRegions();
        var festivals = _repository.GetFestivals();
        var regionsById = regions.ToDictionary(item => item.Id);
        var festivalsById = festivals.ToDictionary(item => item.Id);

        var entries = _repository.GetEntries().AsEnumerable();

        if (regionId.HasValue)
        {
            var regionTreeIds = _repository.GetRegionTreeIds(regionId.Value).ToHashSet();
            entries = entries.Where(item => regionTreeIds.Contains(item.RegionId));
        }

        if (festivalId.HasValue)
        {
            entries = entries.Where(item => item.FestivalId == festivalId.Value);
        }

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var normalizedKeyword = keyword.Trim();
            entries = entries.Where(item =>
                MatchesKeyword(
                    item,
                    normalizedKeyword,
                    regionsById.GetValueOrDefault(item.RegionId),
                    festivalsById.GetValueOrDefault(item.FestivalId)));
        }

        var viewModel = new EntrySearchViewModel
        {
            Keyword = keyword,
            RegionId = regionId,
            FestivalId = festivalId,
            Entries = entries.ToList(),
            RegionsById = regionsById,
            FestivalsById = festivalsById,
            RegionOptions = RegionPresentation.BuildRegionOptions(regions, regionId, "国家"),
            FestivalOptions = BuildFestivalOptions(festivals, festivalId),
            SelectedRegionLabel = regionId.HasValue && regionsById.TryGetValue(regionId.Value, out var selectedRegion)
                ? selectedRegion.FullPath
                : null
        };

        return View(viewModel);
    }

    public IActionResult Details(int id)
    {
        var entry = _repository.GetEntryById(id);
        if (entry is null)
        {
            return NotFound();
        }

        var region = _repository.GetRegionById(entry.RegionId);
        var festival = _repository.GetFestivalById(entry.FestivalId);
        if (region is null || festival is null)
        {
            return NotFound();
        }

        var viewModel = new EntryDetailsViewModel
        {
            Entry = entry,
            Region = region,
            Festival = festival,
            Sources = _repository.GetSourcesForEntry(id)
        };

        return View(viewModel);
    }

    private static IReadOnlyList<SelectListItem> BuildFestivalOptions(IReadOnlyList<Festival> festivals, int? selectedId) =>
        festivals
            .Select(item => new SelectListItem($"{item.Name} · {item.LunarLabel}", item.Id.ToString(), item.Id == selectedId))
            .ToList();

    private static bool MatchesKeyword(FolkloreEntry entry, string keyword, Region? region, Festival? festival) =>
        entry.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
        entry.Summary.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
        entry.HistoricalOrigin.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
        entry.SymbolicMeaning.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
        entry.OralText.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
        entry.ItemsUsed.Any(value => value.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
        entry.Taboos.Any(value => value.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
        entry.Participants.Any(value => value.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
        (region is not null && (
            region.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            region.FullPath.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            region.CulturalFocus.Contains(keyword, StringComparison.OrdinalIgnoreCase))) ||
        (festival is not null && (
            festival.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            festival.LunarLabel.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            festival.Summary.Contains(keyword, StringComparison.OrdinalIgnoreCase)));
}
