using Microsoft.AspNetCore.Mvc;
using MinsuXize.Web.Services;
using MinsuXize.Web.ViewModels;

namespace MinsuXize.Web.Controllers;

public sealed class FestivalsController : Controller
{
    private readonly IFolkloreRepository _repository;

    public FestivalsController(IFolkloreRepository repository)
    {
        _repository = repository;
    }

    public IActionResult Index()
    {
        ViewData["MetaDescription"] = "按节日、节气和时间节点查看不同地区的民俗差异，横向比较相同节日下的具体做法。";
        return View(_repository.GetFestivals());
    }

    public IActionResult Details(int id)
    {
        var festival = _repository.GetFestivalById(id);
        if (festival is null)
        {
            return NotFound();
        }

        ViewData["MetaDescription"] = festival.Summary;

        var viewModel = new FestivalDetailsViewModel
        {
            Festival = festival,
            Entries = _repository.GetEntriesByFestival(id)
        };

        return View(viewModel);
    }
}
