using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MinsuXize.Web.Models;
using MinsuXize.Web.Services;
using MinsuXize.Web.ViewModels;

namespace MinsuXize.Web.Controllers;

[Route("rituals")]
public sealed class RitualsController : Controller
{
    private readonly IFolkloreRepository _repository;

    public RitualsController(IFolkloreRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("")]
    public IActionResult Index(string? keyword, int? regionId, int? festivalId, string? tag)
    {
        var regions = _repository.GetRegions();
        var festivals = _repository.GetFestivals();
        var allEntries = _repository.GetEntriesByContentType("ritual");
        var regionsById = regions.ToDictionary(item => item.Id);
        var festivalsById = festivals.ToDictionary(item => item.Id);
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

        if (!string.IsNullOrWhiteSpace(tag))
        {
            entries = entries.Where(item => item.Tags.Any(value => value.Equals(tag.Trim(), StringComparison.OrdinalIgnoreCase)));
        }

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var normalizedKeyword = keyword.Trim();
            entries = entries.Where(item =>
                item.Title.Contains(normalizedKeyword, StringComparison.OrdinalIgnoreCase) ||
                item.Summary.Contains(normalizedKeyword, StringComparison.OrdinalIgnoreCase) ||
                item.RitualProcess.Any(value => value.Contains(normalizedKeyword, StringComparison.OrdinalIgnoreCase)) ||
                item.Tags.Any(value => value.Contains(normalizedKeyword, StringComparison.OrdinalIgnoreCase)));
        }

        ViewData["Title"] = "仪式";
        ViewData["MetaDescription"] = "按地区、节日和标签浏览民俗仪式词条。";

        var model = new EntrySearchViewModel
        {
            Keyword = keyword,
            RegionId = regionId,
            FestivalId = festivalId,
            ContentType = "ritual",
            Tag = tag,
            Entries = entries.OrderByDescending(item => item.UpdatedAt).ThenBy(item => item.Title).ToList(),
            RegionsById = regionsById,
            FestivalsById = festivalsById,
            RegionOptions = RegionPresentation.BuildRegionOptions(regions, regionId, "国家"),
            FestivalOptions = festivals.Select(item => new SelectListItem($"{item.Name} | {item.LunarLabel}", item.Id.ToString(), item.Id == festivalId)).ToList(),
            ContentTypeOptions = [new SelectListItem("仪式", "ritual", true)],
            TagOptions = allEntries.SelectMany(item => item.Tags).Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(item => item).Select(item => new SelectListItem(item, item, item.Equals(tag, StringComparison.OrdinalIgnoreCase))).ToList(),
            InheritanceStatusOptions = [],
            SelectedRegionLabel = regionId.HasValue && regionsById.TryGetValue(regionId.Value, out var selectedRegion) ? selectedRegion.FullPath : null
        };

        return View("~/Views/Entries/Index.cshtml", model);
    }

    [HttpGet("{slug}")]
    public IActionResult Details(string slug)
    {
        var entry = _repository.GetEntryBySlug(slug);
        if (entry is null || !entry.ContentType.Equals("ritual", StringComparison.OrdinalIgnoreCase))
        {
            return NotFound();
        }

        var region = _repository.GetRegionById(entry.RegionId);
        var festival = _repository.GetFestivalById(entry.FestivalId);
        if (region is null || festival is null)
        {
            return NotFound();
        }

        ViewData["Title"] = entry.Title;
        ViewData["MetaDescription"] = entry.Summary;

        return View("~/Views/Entries/Details.cshtml", new EntryDetailsViewModel
        {
            Entry = entry,
            Region = region,
            Festival = festival,
            Faqs = _repository.GetFaqsForEntry(entry.Id),
            Sources = _repository.GetSourcesForEntry(entry.Id),
            RelatedEntries = _repository.GetRelatedEntries(entry.Id),
            RegionsById = _repository.GetRegions().ToDictionary(item => item.Id),
            FestivalsById = _repository.GetFestivals().ToDictionary(item => item.Id)
        });
    }
}
