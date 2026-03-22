using Microsoft.EntityFrameworkCore;
using MinsuXize.Web.Data;
using MinsuXize.Web.Data.Entities;
using MinsuXize.Web.Data.Seed;
using MinsuXize.Web.Models;

namespace MinsuXize.Web.Services;

public sealed class EfFolkloreRepository : IFolkloreRepository
{
    private readonly AppDbContext _context;

    public EfFolkloreRepository(AppDbContext context)
    {
        _context = context;
    }

    public IReadOnlyList<Region> GetRegions() =>
        _context.Regions
            .AsNoTracking()
            .OrderBy(item => item.Id)
            .ToList()
            .Select(MapRegion)
            .ToList();

    public Region? GetRegionById(int id)
    {
        var entity = _context.Regions.AsNoTracking().FirstOrDefault(item => item.Id == id);
        return entity is null ? null : MapRegion(entity);
    }

    public IReadOnlyList<Region> GetChildRegions(int parentId) =>
        _context.Regions
            .AsNoTracking()
            .Where(item => item.ParentId == parentId)
            .OrderBy(item => item.Id)
            .ToList()
            .Select(MapRegion)
            .ToList();

    public IReadOnlyList<int> GetRegionTreeIds(int regionId) =>
        RegionPresentation.GetRegionTreeIds(GetRegions(), regionId);

    public IReadOnlyList<Festival> GetFestivals() =>
        _context.Festivals
            .AsNoTracking()
            .OrderBy(item => item.Id)
            .ToList()
            .Select(MapFestival)
            .ToList();

    public Festival? GetFestivalById(int id)
    {
        var entity = _context.Festivals.AsNoTracking().FirstOrDefault(item => item.Id == id);
        return entity is null ? null : MapFestival(entity);
    }

    public IReadOnlyList<FolkloreEntry> GetEntries() =>
        _context.Entries
            .AsNoTracking()
            .OrderBy(item => item.Id)
            .ToList()
            .Select(MapEntry)
            .ToList();

    public FolkloreEntry? GetEntryById(int id)
    {
        var entity = _context.Entries.AsNoTracking().FirstOrDefault(item => item.Id == id);
        if (entity is null)
        {
            return null;
        }

        var sourceIds = _context.EntrySources
            .AsNoTracking()
            .Where(item => item.EntryId == id)
            .OrderBy(item => item.Id)
            .Select(item => item.SourceId)
            .ToList();

        return MapEntry(entity, sourceIds);
    }

    public IReadOnlyList<FolkloreEntry> GetEntriesByRegion(int regionId)
    {
        var regionTreeIds = GetRegionTreeIds(regionId);

        return _context.Entries
            .AsNoTracking()
            .Where(item => regionTreeIds.Contains(item.RegionId))
            .OrderBy(item => item.Title)
            .ToList()
            .Select(MapEntry)
            .ToList();
    }

    public IReadOnlyList<FolkloreEntry> GetEntriesByFestival(int festivalId) =>
        _context.Entries
            .AsNoTracking()
            .Where(item => item.FestivalId == festivalId)
            .OrderBy(item => item.Title)
            .ToList()
            .Select(MapEntry)
            .ToList();

    public IReadOnlyList<SourceEvidence> GetSourcesForEntry(int entryId)
    {
        var sourceIds = _context.EntrySources
            .AsNoTracking()
            .Where(item => item.EntryId == entryId)
            .Select(item => item.SourceId)
            .ToList();

        return _context.Sources
            .AsNoTracking()
            .Where(item => sourceIds.Contains(item.Id))
            .OrderBy(item => item.Id)
            .ToList()
            .Select(MapSource)
            .ToList();
    }

    public IReadOnlyList<SubmissionRecord> GetSubmissions() =>
        _context.Submissions
            .AsNoTracking()
            .OrderByDescending(item => item.SubmittedAtUtc)
            .ToList()
            .Select(MapSubmission)
            .ToList();

    public SubmissionRecord? GetSubmissionById(int id)
    {
        var entity = _context.Submissions.AsNoTracking().FirstOrDefault(item => item.Id == id);
        return entity is null ? null : MapSubmission(entity);
    }

    public int GetPendingSubmissionCount() =>
        _context.Submissions.Count(item => item.Status == (int)SubmissionStatus.PendingReview);

    public int CreateSubmission(SubmissionInput input)
    {
        var entity = new SubmissionRecordEntity
        {
            ContributorName = input.ContributorName,
            RegionId = input.RegionId,
            FestivalId = input.FestivalId,
            Title = input.Title,
            Summary = input.Summary,
            SourceSummary = input.SourceSummary,
            Contact = input.Contact,
            SubmittedAtUtc = DateTime.UtcNow,
            Status = (int)SubmissionStatus.PendingReview
        };

        _context.Submissions.Add(entity);
        _context.SaveChanges();
        return entity.Id;
    }

    public void UpdateSubmissionStatus(int submissionId, SubmissionStatus status, string? reviewerNote)
    {
        var entity = _context.Submissions.FirstOrDefault(item => item.Id == submissionId);
        if (entity is null)
        {
            return;
        }

        entity.Status = (int)status;
        entity.ReviewerNote = reviewerNote;
        _context.SaveChanges();
    }

    private static Region MapRegion(RegionEntity entity) =>
        new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Type = entity.Type,
            ParentId = entity.ParentId,
            FullPath = entity.FullPath,
            Summary = entity.Summary,
            CulturalFocus = entity.CulturalFocus,
            Highlights = JsonListSerializer.Deserialize(entity.HighlightsJson)
        };

    private static Festival MapFestival(FestivalEntity entity) =>
        new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Category = entity.Category,
            LunarLabel = entity.LunarLabel,
            Summary = entity.Summary,
            CoreTopics = JsonListSerializer.Deserialize(entity.CoreTopicsJson)
        };

    private FolkloreEntry MapEntry(FolkloreEntryEntity entity)
    {
        var sourceIds = _context.EntrySources
            .AsNoTracking()
            .Where(item => item.EntryId == entity.Id)
            .OrderBy(item => item.Id)
            .Select(item => item.SourceId)
            .ToList();

        return MapEntry(entity, sourceIds);
    }

    private static FolkloreEntry MapEntry(FolkloreEntryEntity entity, IReadOnlyList<int> sourceIds) =>
        new()
        {
            Id = entity.Id,
            Title = entity.Title,
            RegionId = entity.RegionId,
            FestivalId = entity.FestivalId,
            Summary = entity.Summary,
            HistoricalOrigin = entity.HistoricalOrigin,
            SymbolicMeaning = entity.SymbolicMeaning,
            InheritanceStatus = entity.InheritanceStatus,
            ChangeNotes = entity.ChangeNotes,
            OralText = entity.OralText,
            RitualProcess = JsonListSerializer.Deserialize(entity.RitualProcessJson),
            ItemsUsed = JsonListSerializer.Deserialize(entity.ItemsUsedJson),
            Taboos = JsonListSerializer.Deserialize(entity.TaboosJson),
            Participants = JsonListSerializer.Deserialize(entity.ParticipantsJson),
            SourceIds = sourceIds
        };

    private static SourceEvidence MapSource(SourceEvidenceEntity entity) =>
        new()
        {
            Id = entity.Id,
            SourceType = entity.SourceType,
            Title = entity.Title,
            Contributor = entity.Contributor,
            RecordedAt = entity.RecordedAt,
            Citation = entity.Citation,
            Url = entity.Url,
            Notes = entity.Notes
        };

    private static SubmissionRecord MapSubmission(SubmissionRecordEntity entity) =>
        new()
        {
            Id = entity.Id,
            ContributorName = entity.ContributorName,
            RegionId = entity.RegionId,
            FestivalId = entity.FestivalId,
            Title = entity.Title,
            Summary = entity.Summary,
            SourceSummary = entity.SourceSummary,
            Contact = entity.Contact,
            SubmittedAt = DateTime.SpecifyKind(entity.SubmittedAtUtc, DateTimeKind.Utc),
            Status = (SubmissionStatus)entity.Status,
            ReviewerNote = entity.ReviewerNote
        };
}
