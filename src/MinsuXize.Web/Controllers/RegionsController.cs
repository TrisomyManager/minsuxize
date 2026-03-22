using Microsoft.AspNetCore.Mvc;
using MinsuXize.Web.Services;
using MinsuXize.Web.ViewModels;

namespace MinsuXize.Web.Controllers;

public sealed class RegionsController : Controller
{
    private readonly IFolkloreRepository _repository;

    public RegionsController(IFolkloreRepository repository)
    {
        _repository = repository;
    }

    public IActionResult Index(string? keyword)
    {
        var regions = _repository.GetRegions();
        var trimmedKeyword = string.IsNullOrWhiteSpace(keyword) ? null : keyword.Trim();

        IReadOnlyList<MinsuXize.Web.Models.Region> displayRegions = trimmedKeyword is null
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

    public IActionResult Details(int id)
    {
        var region = _repository.GetRegionById(id);
        if (region is null)
        {
            return NotFound();
        }

        var viewModel = new RegionDetailsViewModel
        {
            Region = region,
            ParentRegion = region.ParentId is int parentId ? _repository.GetRegionById(parentId) : null,
            ChildRegions = _repository.GetChildRegions(id),
            Entries = _repository.GetEntriesByRegion(id),
            DescendantRegionCount = Math.Max(0, _repository.GetRegionTreeIds(id).Count - 1)
        };

        return View(viewModel);
    }
}
