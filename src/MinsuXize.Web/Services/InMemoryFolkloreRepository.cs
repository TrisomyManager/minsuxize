using MinsuXize.Web.Models;

namespace MinsuXize.Web.Services;

public sealed class InMemoryFolkloreRepository : IFolkloreRepository
{
    private readonly List<Region> _regions =
    [
        new()
        {
            Id = 1,
            Slug = "china",
            Name = "中国",
            Type = "国家",
            FullPath = "中国",
            Summary = "中国民俗知识库的根地区。",
            CulturalFocus = "跨地区民俗资料汇聚与比较。",
            Highlights = ["全国检索", "地区索引"],
            UpdatedAt = DateTime.UtcNow
        }
    ];

    private readonly List<Festival> _festivals =
    [
        new()
        {
            Id = 1,
            Slug = "spring-festival",
            Name = "春节",
            Category = "岁时节日",
            LunarLabel = "农历正月初一",
            Summary = "年度最重要的传统节日之一。",
            CoreTopics = ["祭祖", "年夜饭", "拜年"],
            UpdatedAt = DateTime.UtcNow
        }
    ];

    private readonly List<FolkloreEntry> _entries = [];
    private readonly List<SubmissionRecord> _submissions = [];
    private readonly List<ReviewHistory> _reviewHistories = [];
    private int _submissionId;

    public IReadOnlyList<Region> GetRegions() => _regions;
    public Region? GetRegionById(int id) => _regions.FirstOrDefault(item => item.Id == id);
    public Region? GetRegionBySlug(string slug) => _regions.FirstOrDefault(item => item.Slug == slug);
    public IReadOnlyList<Region> GetChildRegions(int parentId) => _regions.Where(item => item.ParentId == parentId).ToList();
    public IReadOnlyList<int> GetRegionTreeIds(int regionId) => [regionId];
    public IReadOnlyList<Festival> GetFestivals() => _festivals;
    public Festival? GetFestivalById(int id) => _festivals.FirstOrDefault(item => item.Id == id);
    public Festival? GetFestivalBySlug(string slug) => _festivals.FirstOrDefault(item => item.Slug == slug);
    public IReadOnlyList<FolkloreEntry> GetEntries() => _entries;
    public IReadOnlyList<FolkloreEntry> GetEntriesByContentType(string contentType) => _entries.Where(item => item.ContentType == contentType).ToList();
    public FolkloreEntry? GetEntryById(int id) => _entries.FirstOrDefault(item => item.Id == id);
    public FolkloreEntry? GetEntryBySlug(string slug) => _entries.FirstOrDefault(item => item.Slug == slug);
    public IReadOnlyList<FolkloreEntry> GetEntriesByRegion(int regionId) => _entries.Where(item => item.RegionId == regionId).ToList();
    public IReadOnlyList<FolkloreEntry> GetEntriesByFestival(int festivalId) => _entries.Where(item => item.FestivalId == festivalId).ToList();
    public IReadOnlyList<FolkloreEntry> GetRelatedEntries(int entryId, int take = 4) => [];
    public IReadOnlyList<EntryFaq> GetFaqsForEntry(int entryId) => [];
    public IReadOnlyList<SourceEvidence> GetSourcesForEntry(int entryId) => [];
    public StructuredKnowledgeEntry? GetStructuredEntry(string slug) => null;
    public IReadOnlyList<SubmissionRecord> GetSubmissions() => _submissions;
    public SubmissionRecord? GetSubmissionById(int id) => _submissions.FirstOrDefault(item => item.Id == id);
    public int GetPendingSubmissionCount() => _submissions.Count(item => item.Status == SubmissionStatus.PendingReview);

    public int CreateSubmission(SubmissionInput input)
    {
        var id = ++_submissionId;
        _submissions.Add(new SubmissionRecord
        {
            Id = id,
            RelatedEntryId = input.RelatedEntryId,
            ContentType = input.ContentType,
            ContributorName = input.ContributorName,
            RegionId = input.RegionId,
            FestivalId = input.FestivalId,
            Title = input.Title,
            Summary = input.Summary,
            SourceSummary = input.SourceSummary,
            Contact = input.Contact,
            SubmittedAt = DateTime.UtcNow,
            Status = SubmissionStatus.PendingReview,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CreatedBy = input.ContributorName,
            Version = 1,
            ChangeLog = input.ChangeLog,
            Images = input.Images,
            Videos = input.Videos,
            Audios = input.Audios,
            Location = input.Location
        });
        return id;
    }

    public void UpdateSubmissionStatus(int submissionId, SubmissionStatus status, string? reviewerNote, string reviewerName)
    {
        var item = _submissions.FirstOrDefault(submission => submission.Id == submissionId);
        if (item is null)
        {
            return;
        }
    }

    public IReadOnlyList<ReviewHistory> GetReviewHistory(int submissionId) =>
        _reviewHistories.Where(item => item.SubmissionId == submissionId).ToList();

    public void AddReviewHistory(ReviewHistory history) => _reviewHistories.Add(history);

    public void BulkUpdateSubmissionStatus(List<int> submissionIds, SubmissionStatus status, string? reviewerNote, string reviewerName)
    {
    }
}
