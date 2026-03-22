using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinsuXize.Web.Models;
using MinsuXize.Web.Services;
using MinsuXize.Web.ViewModels;

namespace MinsuXize.Web.Controllers;

[Authorize(Policy = "AdminOnly")]
[Route("manage/review")]
public sealed class AdminController : Controller
{
    private readonly IFolkloreRepository _repository;

    public AdminController(IFolkloreRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        var regions = _repository.GetRegions().ToDictionary(item => item.Id);
        var festivals = _repository.GetFestivals().ToDictionary(item => item.Id);

        ViewData["IsAdminPage"] = true;
        ViewData["AdminUsername"] = User.Identity?.Name;

        var viewModel = new AdminDashboardViewModel
        {
            Submissions = _repository.GetSubmissions(),
            RegionsById = regions,
            FestivalsById = festivals
        };

        return View(viewModel);
    }

    [HttpPost("update")]
    [ValidateAntiForgeryToken]
    public IActionResult Review(int id, SubmissionStatus status, string? reviewerNote)
    {
        _repository.UpdateSubmissionStatus(id, status, reviewerNote);
        return RedirectToAction(nameof(Index));
    }
}
