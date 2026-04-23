using Microsoft.AspNetCore.Mvc;
using MinsuXize.Web.Services;
using MinsuXize.Web.ViewModels;

namespace MinsuXize.Web.Controllers;

[Route("festivals")]
public sealed class FestivalsController : Controller
{
    private readonly IFolkloreRepository _repository;

    public FestivalsController(IFolkloreRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        ViewData["Title"] = "节日";
        ViewData["MetaDescription"] = "按节日、节气和时间节点查看不同地区的民俗差异。";
        return View(_repository.GetFestivals());
    }

    [HttpGet("{id:int}")]
    public IActionResult LegacyDetails(int id)
    {
        var festival = _repository.GetFestivalById(id);
        if (festival is null)
        {
            return NotFound();
        }

        return RedirectToActionPermanent(nameof(Details), new { slug = festival.Slug });
    }

    [HttpGet("{slug}")]
    public IActionResult Details(string slug)
    {
        var festival = _repository.GetFestivalBySlug(slug);
        if (festival is null)
        {
            return NotFound();
        }

        ViewData["Title"] = festival.Name;
        ViewData["MetaDescription"] = festival.Summary;

        return View(new FestivalDetailsViewModel
        {
            Festival = festival,
            Entries = _repository.GetEntriesByFestival(festival.Id),
            RegionsById = _repository.GetRegions().ToDictionary(item => item.Id)
        });
    }
}
