using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MinsuXize.Web.Models;
using MinsuXize.Web.Services;
using MinsuXize.Web.ViewModels;

namespace MinsuXize.Web.Controllers;

public sealed class SubmitController : Controller
{
    private readonly IFolkloreRepository _repository;

    public SubmitController(IFolkloreRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(BuildFormModel(new SubmitEntryViewModel()));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(SubmitEntryViewModel model)
    {
        if (!ModelState.IsValid || model.RegionId is null || model.FestivalId is null)
        {
            return View(BuildFormModel(model));
        }

        var submissionId = _repository.CreateSubmission(new SubmissionInput
        {
            ContributorName = model.ContributorName,
            RegionId = model.RegionId.Value,
            FestivalId = model.FestivalId.Value,
            Title = model.Title,
            Summary = model.Summary,
            SourceSummary = model.SourceSummary,
            Contact = model.Contact
        });

        return RedirectToAction(nameof(Thanks), new { id = submissionId });
    }

    [HttpGet]
    public IActionResult Thanks(int id)
    {
        var submission = _repository.GetSubmissionById(id);
        if (submission is null)
        {
            return NotFound();
        }

        return View(submission);
    }

    private SubmitEntryViewModel BuildFormModel(SubmitEntryViewModel model)
    {
        model.Regions = RegionPresentation.BuildRegionOptions(_repository.GetRegions(), model.RegionId, "国家");

        model.Festivals = _repository.GetFestivals()
            .Select(item => new SelectListItem($"{item.Name} · {item.LunarLabel}", item.Id.ToString()))
            .ToList();

        return model;
    }
}
