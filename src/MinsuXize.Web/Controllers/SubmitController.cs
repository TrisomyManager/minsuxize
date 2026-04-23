using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MinsuXize.Web.Models;
using MinsuXize.Web.Services;
using MinsuXize.Web.ViewModels;

namespace MinsuXize.Web.Controllers;

[Route("submit")]
public sealed class SubmitController : Controller
{
    private readonly IFolkloreRepository _repository;

    public SubmitController(IFolkloreRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("")]
    [HttpGet("/feedback")]
    public IActionResult Create(int? entryId)
    {
        ViewData["Title"] = "投稿与纠错";
        ViewData["MetaDescription"] = "提交民俗词条补充、来源线索或纠错信息，进入待核实整理流程。";

        var model = new SubmitEntryViewModel();
        if (entryId.HasValue)
        {
            var entry = _repository.GetEntryById(entryId.Value);
            if (entry is not null)
            {
                model.RelatedEntryId = entry.Id;
                model.RelatedEntryTitle = entry.Title;
                model.ContentType = entry.ContentType;
                model.RegionId = entry.RegionId;
                model.FestivalId = entry.FestivalId;
                model.Title = entry.Title;
                model.ChangeLog = "针对现有公开词条补充或纠错";
            }
        }

        return View(BuildFormModel(model));
    }

    [HttpPost("")]
    [HttpPost("/feedback")]
    [ValidateAntiForgeryToken]
    public IActionResult Create(SubmitEntryViewModel model)
    {
        ViewData["Title"] = "投稿与纠错";
        ViewData["MetaDescription"] = "提交民俗词条补充、来源线索或纠错信息，进入待核实整理流程。";

        if (model.RelatedEntryId.HasValue)
        {
            var relatedEntry = _repository.GetEntryById(model.RelatedEntryId.Value);
            model.RelatedEntryTitle = relatedEntry?.Title;
        }

        if (!ModelState.IsValid || model.RegionId is null || model.FestivalId is null)
        {
            return View(BuildFormModel(model));
        }

        var submissionId = _repository.CreateSubmission(new SubmissionInput
        {
            RelatedEntryId = model.RelatedEntryId,
            ContentType = model.ContentType,
            ContributorName = model.ContributorName,
            RegionId = model.RegionId.Value,
            FestivalId = model.FestivalId.Value,
            Title = model.Title,
            Summary = model.Summary,
            SourceSummary = model.SourceSummary,
            Contact = model.Contact,
            Images = model.GetImagesList(),
            Videos = model.GetVideosList(),
            Audios = model.GetAudiosList(),
            Location = model.GetLocationInfo(),
            ChangeLog = BuildChangeLog(model)
        });

        return RedirectToAction(nameof(Thanks), new { id = submissionId });
    }

    [HttpGet("thanks/{id:int}")]
    public IActionResult Thanks(int id)
    {
        var submission = _repository.GetSubmissionById(id);
        if (submission is null)
        {
            return NotFound();
        }

        ViewData["Title"] = "提交成功";
        ViewData["MetaDescription"] = $"你提交的“{submission.Title}”已经收到，后续会进入整理和核对流程。";

        return View(submission);
    }

    private SubmitEntryViewModel BuildFormModel(SubmitEntryViewModel model)
    {
        model.Regions = RegionPresentation.BuildRegionOptions(_repository.GetRegions(), model.RegionId, "国家");
        model.Festivals = _repository.GetFestivals()
            .Select(item => new SelectListItem($"{item.Name} | {item.LunarLabel}", item.Id.ToString(), item.Id == model.FestivalId))
            .ToList();
        model.ContentTypes =
        [
            new SelectListItem("仪式", "ritual", model.ContentType == "ritual"),
            new SelectListItem("节日", "festival", model.ContentType == "festival"),
            new SelectListItem("地区", "place", model.ContentType == "place")
        ];

        return model;
    }

    private static string? BuildChangeLog(SubmitEntryViewModel model)
    {
        var relationPrefix = model.RelatedEntryId.HasValue && !string.IsNullOrWhiteSpace(model.RelatedEntryTitle)
            ? $"关联词条 #{model.RelatedEntryId}：{model.RelatedEntryTitle}"
            : null;

        if (string.IsNullOrWhiteSpace(relationPrefix))
        {
            return model.ChangeLog;
        }

        if (string.IsNullOrWhiteSpace(model.ChangeLog))
        {
            return relationPrefix;
        }

        return $"{relationPrefix}；{model.ChangeLog}";
    }
}
