using Microsoft.AspNetCore.Mvc;
using MinsuXize.Web.Models;
using MinsuXize.Web.Services;
using MinsuXize.Web.ViewModels;

namespace MinsuXize.Web.Controllers;

[Route("places")]
public sealed class PlacesController : Controller
{
    private readonly IFolkloreRepository _repository;

    public PlacesController(IFolkloreRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("")]
    public IActionResult Index(string? keyword)
    {
        var regions = _repository.GetRegions();
        var trimmedKeyword = string.IsNullOrWhiteSpace(keyword) ? null : keyword.Trim();

        ViewData["Title"] = "地区";
        ViewData["MetaDescription"] = "从省、市、区县、乡镇和村落等地区层级进入民俗词条。";

        IReadOnlyList<Region> displayRegions = trimmedKeyword is null
            ? regions.Where(item => item.ParentId == 1).OrderBy(item => item.Id).ToList()
            : regions
                .Where(item =>
                    item.Id != 1 &&
                    (item.Name.Contains(trimmedKeyword, StringComparison.OrdinalIgnoreCase) ||
                     item.FullPath.Contains(trimmedKeyword, StringComparison.OrdinalIgnoreCase) ||
                     item.Summary.Contains(trimmedKeyword, StringComparison.OrdinalIgnoreCase)))
                .OrderBy(item => item.FullPath)
                .ToList();

        return View(new RegionIndexViewModel
        {
            Keyword = trimmedKeyword,
            RegionCount = regions.Count - 1,
            Regions = displayRegions
        });
    }

    [HttpGet("{id:int}")]
    public IActionResult LegacyDetails(int id)
    {
        var region = _repository.GetRegionById(id);
        if (region is null)
        {
            return NotFound();
        }

        return RedirectToActionPermanent(nameof(Details), new { slug = region.Slug });
    }

    [HttpGet("{slug}")]
    public IActionResult Details(string slug)
    {
        var region = _repository.GetRegionBySlug(slug);
        if (region is null)
        {
            return NotFound();
        }

        ViewData["Title"] = region.Name;
        ViewData["MetaDescription"] = region.Summary;

        return View(new RegionDetailsViewModel
        {
            Region = region,
            ParentRegion = region.ParentId is int parentId ? _repository.GetRegionById(parentId) : null,
            ChildRegions = _repository.GetChildRegions(region.Id),
            Entries = _repository.GetEntriesByRegion(region.Id),
            DescendantRegionCount = Math.Max(0, _repository.GetRegionTreeIds(region.Id).Count - 1)
        });
    }
}
