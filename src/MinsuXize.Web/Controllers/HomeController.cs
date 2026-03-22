using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MinsuXize.Web.Models;
using MinsuXize.Web.Services;
using MinsuXize.Web.ViewModels;

namespace MinsuXize.Web.Controllers;

public class HomeController : Controller
{
    private readonly IFolkloreRepository _repository;

    public HomeController(IFolkloreRepository repository)
    {
        _repository = repository;
    }

    public IActionResult Index()
    {
        var regions = _repository.GetRegions();
        var festivals = _repository.GetFestivals();
        var entries = _repository.GetEntries();

        var viewModel = new HomePageViewModel
        {
            RegionCount = regions.Count,
            FestivalCount = festivals.Count,
            EntryCount = entries.Count,
            PendingSubmissionCount = _repository.GetPendingSubmissionCount(),
            FeaturedRegions = regions.Where(item => item.ParentId == 1).Take(3).ToList(),
            FeaturedFestivals = festivals.Take(3).ToList(),
            FeaturedEntries = entries.Take(3).ToList(),
            RegionOptions = RegionPresentation.BuildRegionOptions(regions, null, "国家"),
            FestivalOptions = BuildFestivalOptions(festivals)
        };

        return View(viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private static IReadOnlyList<SelectListItem> BuildFestivalOptions(IReadOnlyList<Festival> festivals) =>
        festivals
            .Select(item => new SelectListItem($"{item.Name} · {item.LunarLabel}", item.Id.ToString()))
            .ToList();
}
