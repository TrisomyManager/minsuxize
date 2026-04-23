using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MinsuXize.Web.Models;
using MinsuXize.Web.Services;
using MinsuXize.Web.ViewModels;

namespace MinsuXize.Web.Controllers;

[Route("entries")]
public sealed class EntriesController : Controller
{
    private readonly IFolkloreRepository _repository;

    public EntriesController(IFolkloreRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("")]
    public IActionResult Index(string? keyword, int? regionId, int? festivalId, string? contentType, string? tag, string? inheritanceStatus)
    {
        var regions = _repository.GetRegions();
        var festivals = _repository.GetFestivals();
        var allEntries = _repository.GetEntries();
        var regionsById = regions.ToDictionary(item => item.Id);
        var festivalsById = festivals.ToDictionary(item => item.Id);

        ViewData["Title"] = "民俗词条";
        ViewData["MetaDescription"] = "按关键词、地区、内容类型、节日与标签筛选结构化民俗词条。";

        var entries = FilterEntries(allEntries, regionsById, festivalsById, keyword, regionId, festivalId, contentType, tag, inheritanceStatus);

        var viewModel = new EntrySearchViewModel
        {
            Keyword = keyword,
            RegionId = regionId,
            FestivalId = festivalId,
            ContentType = contentType,
            Tag = tag,
            InheritanceStatus = inheritanceStatus,
            Entries = entries,
            RegionsById = regionsById,
            FestivalsById = festivalsById,
            RegionOptions = RegionPresentation.BuildRegionOptions(regions, regionId, "国家"),
            FestivalOptions = BuildFestivalOptions(festivals, festivalId),
            ContentTypeOptions = BuildContentTypeOptions(allEntries, contentType),
            TagOptions = BuildTagOptions(allEntries, tag),
            InheritanceStatusOptions = BuildInheritanceStatusOptions(allEntries, inheritanceStatus),
            SelectedRegionLabel = regionId.HasValue && regionsById.TryGetValue(regionId.Value, out var selectedRegion)
                ? selectedRegion.FullPath
                : null
        };

        return View(viewModel);
    }

    [HttpGet("{id:int}")]
    public IActionResult LegacyDetails(int id)
    {
        var entry = _repository.GetEntryById(id);
        if (entry is null)
        {
            return NotFound();
        }

        return RedirectToActionPermanent(nameof(Details), new { slug = entry.Slug });
    }

    [HttpGet("{slug}")]
    public IActionResult Details(string slug)
    {
        var viewModel = BuildDetailsViewModel(slug);
        if (viewModel is null)
        {
            return NotFound();
        }

        ViewData["Title"] = viewModel.Entry.Title;
        ViewData["MetaDescription"] = viewModel.Entry.Summary;
        return View(viewModel);
    }

    internal EntryDetailsViewModel? BuildDetailsViewModel(string slug)
    {
        var entry = _repository.GetEntryBySlug(slug);
        if (entry is null)
        {
            return null;
        }

        var region = _repository.GetRegionById(entry.RegionId);
        var festival = _repository.GetFestivalById(entry.FestivalId);
        if (region is null || festival is null)
        {
            return null;
        }

        return new EntryDetailsViewModel
        {
            Entry = entry,
            Region = region,
            Festival = festival,
            Faqs = _repository.GetFaqsForEntry(entry.Id),
            Sources = _repository.GetSourcesForEntry(entry.Id),
            RelatedEntries = _repository.GetRelatedEntries(entry.Id),
            RegionsById = _repository.GetRegions().ToDictionary(item => item.Id),
            FestivalsById = _repository.GetFestivals().ToDictionary(item => item.Id)
        };
    }

    private IReadOnlyList<FolkloreEntry> FilterEntries(
        IReadOnlyList<FolkloreEntry> allEntries,
        IReadOnlyDictionary<int, Region> regionsById,
        IReadOnlyDictionary<int, Festival> festivalsById,
        string? keyword,
        int? regionId,
        int? festivalId,
        string? contentType,
        string? tag,
        string? inheritanceStatus)
    {
        var entries = allEntries.AsEnumerable();

        if (regionId.HasValue)
        {
            var regionTreeIds = _repository.GetRegionTreeIds(regionId.Value).ToHashSet();
            entries = entries.Where(item => regionTreeIds.Contains(item.RegionId));
        }

        if (festivalId.HasValue)
        {
            entries = entries.Where(item => item.FestivalId == festivalId.Value);
        }

        if (!string.IsNullOrWhiteSpace(contentType))
        {
            var normalizedType = contentType.Trim();
            entries = entries.Where(item => item.ContentType.Equals(normalizedType, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(tag))
        {
            var normalizedTag = tag.Trim();
            entries = entries.Where(item => item.Tags.Any(value => value.Equals(normalizedTag, StringComparison.OrdinalIgnoreCase)));
        }

        if (!string.IsNullOrWhiteSpace(inheritanceStatus))
        {
            var normalizedStatus = inheritanceStatus.Trim();
            entries = entries.Where(item => item.InheritanceStatus.Equals(normalizedStatus, StringComparison.OrdinalIgnoreCase));
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

        return entries
            .OrderByDescending(item => item.UpdatedAt)
            .ThenBy(item => item.Title)
            .ToList();
    }

    private static IReadOnlyList<SelectListItem> BuildFestivalOptions(IReadOnlyList<Festival> festivals, int? selectedId) =>
        festivals
            .Select(item => new SelectListItem($"{item.Name} | {item.LunarLabel}", item.Id.ToString(), item.Id == selectedId))
            .ToList();

    private static IReadOnlyList<SelectListItem> BuildContentTypeOptions(IReadOnlyList<FolkloreEntry> entries, string? selectedType) =>
        entries
            .Select(item => item.ContentType)
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(value => value, StringComparer.OrdinalIgnoreCase)
            .Select(value => new SelectListItem(ContentTypeLabel(value), value, value.Equals(selectedType, StringComparison.OrdinalIgnoreCase)))
            .ToList();

    private static IReadOnlyList<SelectListItem> BuildTagOptions(IReadOnlyList<FolkloreEntry> entries, string? selectedTag) =>
        entries
            .SelectMany(item => item.Tags)
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(value => value, StringComparer.OrdinalIgnoreCase)
            .Select(value => new SelectListItem(value, value, value.Equals(selectedTag, StringComparison.OrdinalIgnoreCase)))
            .ToList();

    private static IReadOnlyList<SelectListItem> BuildInheritanceStatusOptions(IReadOnlyList<FolkloreEntry> entries, string? selectedStatus) =>
        entries
            .Select(item => item.InheritanceStatus)
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(value => value, StringComparer.OrdinalIgnoreCase)
            .Select(value => new SelectListItem(value, value, value.Equals(selectedStatus, StringComparison.OrdinalIgnoreCase)))
            .ToList();

    private static bool MatchesKeyword(FolkloreEntry entry, string keyword, Region? region, Festival? festival) =>
        entry.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
        entry.Slug.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
        entry.ContentType.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
        entry.Summary.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
        entry.HistoricalOrigin.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
        entry.SymbolicMeaning.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
        entry.OralText.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
        entry.ItemsUsed.Any(value => value.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
        entry.Preparations.Any(value => value.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
        entry.RitualProcess.Any(value => value.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
        entry.RegionalDifferences.Any(value => value.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
        entry.Taboos.Any(value => value.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
        entry.Tags.Any(value => value.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
        (region is not null && (
            region.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            region.FullPath.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            region.CulturalFocus.Contains(keyword, StringComparison.OrdinalIgnoreCase))) ||
        (festival is not null && (
            festival.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            festival.LunarLabel.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            festival.Summary.Contains(keyword, StringComparison.OrdinalIgnoreCase)));

    internal static string ContentTypeLabel(string value) =>
        value.Equals("ritual", StringComparison.OrdinalIgnoreCase) ? "仪式" :
        value.Equals("festival", StringComparison.OrdinalIgnoreCase) ? "节日" :
        value.Equals("place", StringComparison.OrdinalIgnoreCase) ? "地区" :
        value;
}
