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
        return View(_repository.GetFestivals());
    }

    public IActionResult Details(int id)
    {
        var festival = _repository.GetFestivalById(id);
        if (festival is null)
        {
            return NotFound();
        }

        var viewModel = new FestivalDetailsViewModel
        {
            Festival = festival,
            Entries = _repository.GetEntriesByFestival(id)
        };

        return View(viewModel);
    }
}
