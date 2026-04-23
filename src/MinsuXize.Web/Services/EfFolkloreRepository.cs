using Microsoft.EntityFrameworkCore;
using MinsuXize.Web.Data;
using MinsuXize.Web.Data.Entities;
using MinsuXize.Web.Data.Seed;
using MinsuXize.Web.Models;
using System.Text.Json;

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

    public Region? GetRegionBySlug(string slug)
    {
        var entity = _context.Regions.AsNoTracking().FirstOrDefault(item => item.Slug == slug);
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

    public Festival? GetFestivalBySlug(string slug)
    {
        var entity = _context.Festivals.AsNoTracking().FirstOrDefault(item => item.Slug == slug);
        return entity is null ? null : MapFestival(entity);
    }

    public IReadOnlyList<FolkloreEntry> GetEntries() =>
        _context.Entries
            .AsNoTracking()
            .OrderByDescending(item => item.UpdatedAt)
            .ThenBy(item => item.Title)
            .ToList()
            .Select(MapEntry)
            .ToList();

    public IReadOnlyList<FolkloreEntry> GetEntriesByContentType(string contentType) =>
        _context.Entries
            .AsNoTracking()
            .Where(item => item.ContentType == contentType)
            .OrderByDescending(item => item.UpdatedAt)
            .ThenBy(item => item.Title)
            .ToList()
            .Select(MapEntry)
            .ToList();

    public FolkloreEntry? GetEntryById(int id)
    {
        var entity = _context.Entries.AsNoTracking().FirstOrDefault(item => item.Id == id);
        return entity is null ? null : MapEntry(entity);
    }

    public FolkloreEntry? GetEntryBySlug(string slug)
    {
        var entity = _context.Entries.AsNoTracking().FirstOrDefault(item => item.Slug == slug);
        return entity is null ? null : MapEntry(entity);
    }

    public IReadOnlyList<FolkloreEntry> GetEntriesByRegion(int regionId)
    {
        var regionTreeIds = GetRegionTreeIds(regionId);

        return _context.Entries
            .AsNoTracking()
            .Where(item => regionTreeIds.Contains(item.RegionId))
            .OrderByDescending(item => item.UpdatedAt)
            .ThenBy(item => item.Title)
            .ToList()
            .Select(MapEntry)
            .ToList();
    }

    public IReadOnlyList<FolkloreEntry> GetEntriesByFestival(int festivalId) =>
        _context.Entries
            .AsNoTracking()
            .Where(item => item.FestivalId == festivalId)
            .OrderByDescending(item => item.UpdatedAt)
            .ThenBy(item => item.Title)
            .ToList()
            .Select(MapEntry)
            .ToList();

    public IReadOnlyList<FolkloreEntry> GetRelatedEntries(int entryId, int take = 4)
    {
        var entry = _context.Entries.AsNoTracking().FirstOrDefault(item => item.Id == entryId);
        if (entry is null)
        {
            return [];
        }

        var relatedIds = _context.EntryRelations
            .AsNoTracking()
            .Where(item => item.EntryId == entryId)
            .OrderBy(item => item.Id)
            .Select(item => item.RelatedEntryId)
            .ToList();

        var explicitRelated = _context.Entries
            .AsNoTracking()
            .Where(item => relatedIds.Contains(item.Id))
            .ToList()
            .OrderBy(item => relatedIds.IndexOf(item.Id))
            .Select(MapEntry)
            .ToList();

        if (explicitRelated.Count >= take)
        {
            return explicitRelated.Take(take).ToList();
        }

        var fallback = _context.Entries
            .AsNoTracking()
            .Where(item =>
                item.Id != entryId &&
                !relatedIds.Contains(item.Id) &&
                (item.RegionId == entry.RegionId ||
                 item.FestivalId == entry.FestivalId ||
                 item.ContentType == entry.ContentType))
            .OrderByDescending(item => item.FestivalId == entry.FestivalId)
            .ThenByDescending(item => item.RegionId == entry.RegionId)
            .ThenByDescending(item => item.UpdatedAt)
            .Take(take - explicitRelated.Count)
            .ToList()
            .Select(MapEntry);

        return explicitRelated.Concat(fallback).Take(take).ToList();
    }

    public IReadOnlyList<EntryFaq> GetFaqsForEntry(int entryId) =>
        _context.EntryFaqs
            .AsNoTracking()
            .Where(item => item.EntryId == entryId)
            .OrderBy(item => item.SortOrder)
            .ThenBy(item => item.Id)
            .ToList()
            .Select(MapFaq)
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

    public StructuredKnowledgeEntry? GetStructuredEntry(string slug)
    {
        var entry = GetEntryBySlug(slug);
        if (entry is null)
        {
            return null;
        }

        var region = GetRegionById(entry.RegionId);
        var festival = GetFestivalById(entry.FestivalId);
        if (region is null || festival is null)
        {
            return null;
        }

        return new StructuredKnowledgeEntry
        {
            Title = entry.Title,
            Slug = entry.Slug,
            ContentType = entry.ContentType,
            Summary = entry.Summary,
            Region = region,
            Festival = festival,
            RegionFields = entry.RegionFields,
            Materials = entry.ItemsUsed,
            Preparations = entry.Preparations,
            Steps = entry.RitualProcess,
            Taboos = entry.Taboos,
            RegionalDifferences = entry.RegionalDifferences,
            FAQ = GetFaqsForEntry(entry.Id),
            Sources = GetSourcesForEntry(entry.Id),
            ReviewStatus = entry.ReviewStatus,
            ConfidenceLevel = entry.ConfidenceLevel,
            UpdatedAt = entry.UpdatedAt
        };
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
            RelatedEntryId = input.RelatedEntryId,
            ContentType = input.ContentType,
            ContributorName = input.ContributorName,
            RegionId = input.RegionId,
            FestivalId = input.FestivalId,
            Title = input.Title,
            Summary = input.Summary,
            SourceSummary = input.SourceSummary,
            Contact = input.Contact,
            SubmittedAtUtc = DateTime.UtcNow,
            Status = (int)SubmissionStatus.PendingReview,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CreatedBy = input.ContributorName,
            Version = 1,
            ChangeLog = input.ChangeLog,
            Images = input.Images,
            Videos = input.Videos,
            Audios = input.Audios,
            Location = input.Location == null ? null : new LocationInfoData
            {
                Latitude = input.Location.Latitude,
                Longitude = input.Location.Longitude,
                Address = input.Location.Address,
                City = input.Location.City,
                Province = input.Location.Province,
                Country = input.Location.Country,
                Description = input.Location.Description
            }
        };

        _context.Submissions.Add(entity);
        _context.SaveChanges();
        return entity.Id;
    }

    public void UpdateSubmissionStatus(int submissionId, SubmissionStatus status, string? reviewerNote, string reviewerName)
    {
        var entity = _context.Submissions.FirstOrDefault(item => item.Id == submissionId);
        if (entity is null)
        {
            return;
        }

        var oldStatus = (SubmissionStatus)entity.Status;
        entity.Status = (int)status;
        entity.ReviewerNote = reviewerNote;
        entity.UpdatedAt = DateTime.UtcNow;

        _context.ReviewHistories.Add(new ReviewHistoryEntity
        {
            SubmissionId = submissionId,
            OldStatus = (int)oldStatus,
            NewStatus = (int)status,
            Reviewer = reviewerName,
            ReviewerNote = reviewerNote,
            ReviewedAt = DateTime.UtcNow,
            ChangeSummary = $"状态从 {oldStatus} 更新为 {status}"
        });

        _context.SaveChanges();
    }

    public IReadOnlyList<ReviewHistory> GetReviewHistory(int submissionId) =>
        _context.ReviewHistories
            .AsNoTracking()
            .Where(h => h.SubmissionId == submissionId)
            .OrderByDescending(h => h.ReviewedAt)
            .Select(h => new ReviewHistory
            {
                Id = h.Id,
                SubmissionId = h.SubmissionId,
                OldStatus = (SubmissionStatus)h.OldStatus,
                NewStatus = (SubmissionStatus)h.NewStatus,
                Reviewer = h.Reviewer,
                ReviewerNote = h.ReviewerNote,
                ReviewedAt = h.ReviewedAt,
                ChangeSummary = h.ChangeSummary
            })
            .ToList();

    public void AddReviewHistory(ReviewHistory history)
    {
        _context.ReviewHistories.Add(new ReviewHistoryEntity
        {
            SubmissionId = history.SubmissionId,
            OldStatus = (int)history.OldStatus,
            NewStatus = (int)history.NewStatus,
            Reviewer = history.Reviewer,
            ReviewerNote = history.ReviewerNote,
            ReviewedAt = history.ReviewedAt,
            ChangeSummary = history.ChangeSummary,
            MetadataJson = history.MetadataJson
        });

        _context.SaveChanges();
    }

    public void BulkUpdateSubmissionStatus(List<int> submissionIds, SubmissionStatus status, string? reviewerNote, string reviewerName)
    {
        var entities = _context.Submissions
            .Where(s => submissionIds.Contains(s.Id))
            .ToList();

        foreach (var entity in entities)
        {
            var oldStatus = (SubmissionStatus)entity.Status;
            entity.Status = (int)status;
            entity.ReviewerNote = reviewerNote;
            entity.UpdatedAt = DateTime.UtcNow;

            _context.ReviewHistories.Add(new ReviewHistoryEntity
            {
                SubmissionId = entity.Id,
                OldStatus = (int)oldStatus,
                NewStatus = (int)status,
                Reviewer = reviewerName,
                ReviewerNote = reviewerNote,
                ReviewedAt = DateTime.UtcNow,
                ChangeSummary = $"批量操作：状态从 {oldStatus} 更新为 {status}"
            });
        }

        _context.SaveChanges();
    }

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

    private static Region MapRegion(RegionEntity entity) =>
        new()
        {
            Id = entity.Id,
            Slug = entity.Slug,
            Name = entity.Name,
            Type = entity.Type,
            ParentId = entity.ParentId,
            FullPath = entity.FullPath,
            Summary = entity.Summary,
            CulturalFocus = entity.CulturalFocus,
            Highlights = JsonListSerializer.Deserialize(entity.HighlightsJson),
            UpdatedAt = entity.UpdatedAt
        };

    private static Festival MapFestival(FestivalEntity entity) =>
        new()
        {
            Id = entity.Id,
            Slug = entity.Slug,
            Name = entity.Name,
            Category = entity.Category,
            LunarLabel = entity.LunarLabel,
            Summary = entity.Summary,
            CoreTopics = JsonListSerializer.Deserialize(entity.CoreTopicsJson),
            UpdatedAt = entity.UpdatedAt
        };

    private static FolkloreEntry MapEntry(FolkloreEntryEntity entity, IReadOnlyList<int> sourceIds)
    {
        GeoLocation? location = null;
        if (!string.IsNullOrWhiteSpace(entity.LocationJson))
        {
            try
            {
                var locationData = JsonSerializer.Deserialize<GeoLocationData>(entity.LocationJson);
                if (locationData is not null)
                {
                    location = new GeoLocation(locationData.Latitude, locationData.Longitude, locationData.AddressDetail);
                }
            }
            catch
            {
                location = null;
            }
        }

        return new()
        {
            Id = entity.Id,
            Title = entity.Title,
            Slug = entity.Slug,
            ContentType = entity.ContentType,
            RegionId = entity.RegionId,
            FestivalId = entity.FestivalId,
            Summary = entity.Summary,
            RegionFields = DeserializeDictionary(entity.RegionFieldsJson),
            HistoricalOrigin = entity.HistoricalOrigin,
            SymbolicMeaning = entity.SymbolicMeaning,
            InheritanceStatus = entity.InheritanceStatus,
            ChangeNotes = entity.ChangeNotes,
            OralText = entity.OralText,
            Preparations = JsonListSerializer.Deserialize(entity.PreparationsJson),
            RitualProcess = JsonListSerializer.Deserialize(entity.RitualProcessJson),
            RegionalDifferences = JsonListSerializer.Deserialize(entity.RegionalDifferencesJson),
            ItemsUsed = JsonListSerializer.Deserialize(entity.ItemsUsedJson),
            Taboos = JsonListSerializer.Deserialize(entity.TaboosJson),
            Participants = JsonListSerializer.Deserialize(entity.ParticipantsJson),
            Tags = JsonListSerializer.Deserialize(entity.TagsJson),
            SourceIds = sourceIds,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            CreatedBy = entity.CreatedBy,
            Status = entity.Status,
            ReviewStatus = entity.ReviewStatus,
            ConfidenceLevel = entity.ConfidenceLevel,
            SourceGrade = entity.SourceGrade,
            Version = entity.Version,
            ChangeLog = entity.ChangeLog,
            Images = JsonListSerializer.Deserialize(entity.ImagesJson),
            Videos = JsonListSerializer.Deserialize(entity.VideosJson),
            Audios = JsonListSerializer.Deserialize(entity.AudiosJson),
            Location = location
        };
    }

    private static EntryFaq MapFaq(EntryFaqEntity entity) =>
        new()
        {
            Id = entity.Id,
            EntryId = entity.EntryId,
            Question = entity.Question,
            Answer = entity.Answer,
            SortOrder = entity.SortOrder
        };

    private static SourceEvidence MapSource(SourceEvidenceEntity entity) =>
        new()
        {
            Id = entity.Id,
            SourceType = entity.SourceType,
            SourceLevel = entity.SourceLevel,
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
            RelatedEntryId = entity.RelatedEntryId,
            ContentType = entity.ContentType,
            ContributorName = entity.ContributorName,
            RegionId = entity.RegionId,
            FestivalId = entity.FestivalId,
            Title = entity.Title,
            Summary = entity.Summary,
            SourceSummary = entity.SourceSummary,
            Contact = entity.Contact,
            SubmittedAt = DateTime.SpecifyKind(entity.SubmittedAtUtc, DateTimeKind.Utc),
            Status = (SubmissionStatus)entity.Status,
            ReviewerNote = entity.ReviewerNote,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            CreatedBy = entity.CreatedBy,
            Version = entity.Version,
            ChangeLog = entity.ChangeLog,
            Images = entity.Images,
            Videos = entity.Videos,
            Audios = entity.Audios,
            Location = entity.Location == null
                ? null
                : new LocationInfo
                {
                    Latitude = entity.Location.Latitude,
                    Longitude = entity.Location.Longitude,
                    Address = entity.Location.Address,
                    City = entity.Location.City,
                    Province = entity.Location.Province,
                    Country = entity.Location.Country,
                    Description = entity.Location.Description
                }
        };

    private static IReadOnlyDictionary<string, string> DeserializeDictionary(string? json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return new Dictionary<string, string>();
        }

        try
        {
            return JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
        }
        catch
        {
            return new Dictionary<string, string>();
        }
    }

    private sealed class GeoLocationData
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? AddressDetail { get; set; }
    }
}
