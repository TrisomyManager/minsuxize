using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MinsuXize.Web.Data;
using MinsuXize.Web.Data.Entities;
using MinsuXize.Web.Data.Seed;
using MinsuXize.Web.Models;
using MinsuXize.Web.Services;
using MinsuXize.Web.ViewModels;
using System.Text.Json;

namespace MinsuXize.Web.Controllers;

[Authorize(Policy = "AdminOnly")]
[Route("manage/review")]
public sealed class AdminController : Controller
{
    private readonly IFolkloreRepository _repository;
    private readonly AppDbContext _dbContext;

    public AdminController(IFolkloreRepository repository, AppDbContext dbContext)
    {
        _repository = repository;
        _dbContext = dbContext;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        var regions = _repository.GetRegions().ToDictionary(item => item.Id);
        var festivals = _repository.GetFestivals().ToDictionary(item => item.Id);

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
        var reviewerName = User.Identity?.Name ?? "Unknown";
        _repository.UpdateSubmissionStatus(id, status, reviewerNote, reviewerName);
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost("bulk-update")]
    [ValidateAntiForgeryToken]
    public IActionResult BulkReview(string? submissionIdsRaw, SubmissionStatus status, string? reviewerNote)
    {
        var submissionIds = (submissionIdsRaw ?? string.Empty)
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(value => int.TryParse(value, out var parsed) ? parsed : (int?)null)
            .Where(value => value.HasValue)
            .Select(value => value!.Value)
            .Distinct()
            .ToList();

        if (submissionIds.Count == 0)
        {
            return RedirectToAction(nameof(Index));
        }

        var reviewerName = User.Identity?.Name ?? "Unknown";
        _repository.BulkUpdateSubmissionStatus(submissionIds, status, reviewerNote, reviewerName);
        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet("history/{id}")]
    public IActionResult History(int id)
    {
        var submission = _repository.GetSubmissionById(id);
        if (submission is null)
        {
            return NotFound();
        }
        
        var history = _repository.GetReviewHistory(id);
        
        ViewData["AdminUsername"] = User.Identity?.Name;
        
        return View(new ReviewHistoryViewModel
        {
            Submission = submission,
            History = history
        });
    }

    [HttpGet("entries")]
    public IActionResult Entries()
    {
        ViewData["Title"] = "词条维护";
        ViewData["AdminUsername"] = User.Identity?.Name;

        return View(new AdminEntryListViewModel
        {
            Entries = _repository.GetEntries(),
            RegionsById = _repository.GetRegions().ToDictionary(item => item.Id),
            FestivalsById = _repository.GetFestivals().ToDictionary(item => item.Id)
        });
    }

    [HttpGet("entries/{id:int}/edit")]
    public IActionResult EditEntry(int id)
    {
        var entity = _dbContext.Entries.AsNoTracking().FirstOrDefault(item => item.Id == id);
        if (entity is null)
        {
            return NotFound();
        }

        ViewData["Title"] = $"编辑词条：{entity.Title}";
        ViewData["AdminUsername"] = User.Identity?.Name;

        return View(BuildEntryEditModel(entity));
    }

    [HttpPost("entries/{id:int}/edit")]
    [ValidateAntiForgeryToken]
    public IActionResult EditEntry(int id, AdminEntryEditViewModel model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        var entity = _dbContext.Entries.FirstOrDefault(item => item.Id == id);
        if (entity is null)
        {
            return NotFound();
        }

        if (!IsValidRegionFieldsJson(model.RegionFieldsJson))
        {
            ModelState.AddModelError(nameof(model.RegionFieldsJson), "RegionFieldsJson 必须是 JSON 对象，例如 {\"province\":\"山西省\"}。");
        }

        var slugConflict = _dbContext.Entries.Any(item => item.Id != id && item.Slug == model.Slug.Trim());
        if (slugConflict)
        {
            ModelState.AddModelError(nameof(model.Slug), "Slug 已被其他词条使用。");
        }

        if (!ModelState.IsValid)
        {
            ViewData["Title"] = $"编辑词条：{entity.Title}";
            ViewData["AdminUsername"] = User.Identity?.Name;
            HydrateEntryEditOptions(model);
            return View(model);
        }

        entity.Title = model.Title.Trim();
        entity.Slug = model.Slug.Trim();
        entity.ContentType = model.ContentType.Trim();
        entity.RegionId = model.RegionId;
        entity.FestivalId = model.FestivalId;
        entity.Summary = model.Summary.Trim();
        entity.RegionFieldsJson = string.IsNullOrWhiteSpace(model.RegionFieldsJson) ? "{}" : model.RegionFieldsJson.Trim();
        entity.ItemsUsedJson = JsonListSerializer.Serialize(SplitLines(model.MaterialsText).ToArray());
        entity.PreparationsJson = JsonListSerializer.Serialize(SplitLines(model.PreparationsText).ToArray());
        entity.RitualProcessJson = JsonListSerializer.Serialize(SplitLines(model.StepsText).ToArray());
        entity.TaboosJson = JsonListSerializer.Serialize(SplitLines(model.TaboosText).ToArray());
        entity.RegionalDifferencesJson = JsonListSerializer.Serialize(SplitLines(model.RegionalDifferencesText).ToArray());
        entity.ParticipantsJson = JsonListSerializer.Serialize(SplitLines(model.ParticipantsText).ToArray());
        entity.TagsJson = JsonListSerializer.Serialize(SplitLines(model.TagsText).ToArray());
        entity.HistoricalOrigin = model.HistoricalOrigin.Trim();
        entity.SymbolicMeaning = model.SymbolicMeaning.Trim();
        entity.InheritanceStatus = model.InheritanceStatus.Trim();
        entity.ChangeNotes = model.ChangeNotes.Trim();
        entity.OralText = model.OralText.Trim();
        entity.Status = model.Status.Trim();
        entity.ReviewStatus = model.ReviewStatus.Trim();
        entity.ConfidenceLevel = model.ConfidenceLevel.Trim();
        entity.SourceGrade = model.SourceGrade.Trim();
        entity.ChangeLog = model.ChangeLog.Trim();
        entity.UpdatedAt = DateTime.UtcNow;
        entity.Version += 1;

        ReplaceFaqs(entity.Id, model.FaqLines);
        ReplaceRelations(entity.Id, model.RelatedEntrySlugs);

        _dbContext.SaveChanges();
        TempData["AdminNotice"] = "词条已保存。";

        return RedirectToAction(nameof(EditEntry), new { id });
    }

    private AdminEntryEditViewModel BuildEntryEditModel(FolkloreEntryEntity entity)
    {
        var faqLines = _dbContext.EntryFaqs
            .AsNoTracking()
            .Where(item => item.EntryId == entity.Id)
            .OrderBy(item => item.SortOrder)
            .ThenBy(item => item.Id)
            .Select(item => $"{item.Question} | {item.Answer}")
            .ToList();

        var relatedSlugs = _dbContext.EntryRelations
            .AsNoTracking()
            .Where(item => item.EntryId == entity.Id)
            .Join(_dbContext.Entries.AsNoTracking(), relation => relation.RelatedEntryId, entry => entry.Id, (_, entry) => entry.Slug)
            .ToList();

        var model = new AdminEntryEditViewModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Slug = entity.Slug,
            ContentType = entity.ContentType,
            RegionId = entity.RegionId,
            FestivalId = entity.FestivalId,
            Summary = entity.Summary,
            RegionFieldsJson = entity.RegionFieldsJson,
            MaterialsText = JoinLines(JsonListSerializer.Deserialize(entity.ItemsUsedJson)),
            PreparationsText = JoinLines(JsonListSerializer.Deserialize(entity.PreparationsJson)),
            StepsText = JoinLines(JsonListSerializer.Deserialize(entity.RitualProcessJson)),
            TaboosText = JoinLines(JsonListSerializer.Deserialize(entity.TaboosJson)),
            RegionalDifferencesText = JoinLines(JsonListSerializer.Deserialize(entity.RegionalDifferencesJson)),
            ParticipantsText = JoinLines(JsonListSerializer.Deserialize(entity.ParticipantsJson)),
            TagsText = JoinLines(JsonListSerializer.Deserialize(entity.TagsJson)),
            HistoricalOrigin = entity.HistoricalOrigin,
            SymbolicMeaning = entity.SymbolicMeaning,
            InheritanceStatus = entity.InheritanceStatus,
            ChangeNotes = entity.ChangeNotes,
            OralText = entity.OralText,
            Status = entity.Status,
            ReviewStatus = entity.ReviewStatus,
            ConfidenceLevel = entity.ConfidenceLevel,
            SourceGrade = entity.SourceGrade,
            ChangeLog = entity.ChangeLog,
            FaqLines = string.Join(Environment.NewLine, faqLines),
            RelatedEntrySlugs = string.Join(Environment.NewLine, relatedSlugs)
        };

        HydrateEntryEditOptions(model);
        return model;
    }

    private void HydrateEntryEditOptions(AdminEntryEditViewModel model)
    {
        model.RegionOptions = RegionPresentation.BuildRegionOptions(_repository.GetRegions(), model.RegionId, "国家");
        model.FestivalOptions = _repository.GetFestivals()
            .Select(item => new SelectListItem($"{item.Name} | {item.LunarLabel}", item.Id.ToString(), item.Id == model.FestivalId))
            .ToList();
        model.ContentTypeOptions = BuildOptions(["ritual", "festival", "place"], model.ContentType);
        model.ReviewStatusOptions = BuildOptions(["verified", "pending-verification", "needs-source", "draft"], model.ReviewStatus);
        model.ConfidenceLevelOptions = BuildOptions(["high", "medium", "low"], model.ConfidenceLevel);
        model.SourceGradeOptions = BuildOptions(["field-note", "oral-history", "published-source", "compiled-note", "unverified"], model.SourceGrade);
    }

    private void ReplaceFaqs(int entryId, string? faqLines)
    {
        var existing = _dbContext.EntryFaqs.Where(item => item.EntryId == entryId);
        _dbContext.EntryFaqs.RemoveRange(existing);

        var rows = SplitLines(faqLines).ToList();
        for (var index = 0; index < rows.Count; index++)
        {
            var parts = rows[index].Split('|', 2, StringSplitOptions.TrimEntries);
            if (parts.Length != 2 || string.IsNullOrWhiteSpace(parts[0]) || string.IsNullOrWhiteSpace(parts[1]))
            {
                continue;
            }

            _dbContext.EntryFaqs.Add(new EntryFaqEntity
            {
                EntryId = entryId,
                Question = parts[0],
                Answer = parts[1],
                SortOrder = index + 1
            });
        }
    }

    private void ReplaceRelations(int entryId, string? relatedEntrySlugs)
    {
        var existing = _dbContext.EntryRelations.Where(item => item.EntryId == entryId);
        _dbContext.EntryRelations.RemoveRange(existing);

        var slugs = SplitLines(relatedEntrySlugs).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
        var relatedEntries = _dbContext.Entries
            .Where(item => item.Id != entryId && slugs.Contains(item.Slug))
            .Select(item => new { item.Id, item.Slug })
            .ToList();

        foreach (var relatedEntry in relatedEntries)
        {
            _dbContext.EntryRelations.Add(new EntryRelationEntity
            {
                EntryId = entryId,
                RelatedEntryId = relatedEntry.Id,
                RelationType = "related",
                Note = "后台维护"
            });
        }
    }

    private static bool IsValidRegionFieldsJson(string? json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return true;
        }

        try
        {
            using var document = JsonDocument.Parse(json);
            return document.RootElement.ValueKind == JsonValueKind.Object;
        }
        catch
        {
            return false;
        }
    }

    private static IReadOnlyList<string> SplitLines(string? value) =>
        (value ?? string.Empty)
            .Split(["\r\n", "\n", "\r"], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Where(item => !string.IsNullOrWhiteSpace(item))
            .ToList();

    private static string JoinLines(IEnumerable<string> values) => string.Join(Environment.NewLine, values);

    private static IReadOnlyList<SelectListItem> BuildOptions(IReadOnlyList<string> values, string? selectedValue) =>
        values.Select(value => new SelectListItem(value, value, value.Equals(selectedValue, StringComparison.OrdinalIgnoreCase))).ToList();
}
