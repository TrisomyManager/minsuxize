using MinsuXize.Web.Models;

namespace MinsuXize.Web.Services;

public interface IFolkloreRepository
{
    IReadOnlyList<Region> GetRegions();
    Region? GetRegionById(int id);
    Region? GetRegionBySlug(string slug);
    IReadOnlyList<Region> GetChildRegions(int parentId);
    IReadOnlyList<int> GetRegionTreeIds(int regionId);

    IReadOnlyList<Festival> GetFestivals();
    Festival? GetFestivalById(int id);
    Festival? GetFestivalBySlug(string slug);

    IReadOnlyList<FolkloreEntry> GetEntries();
    IReadOnlyList<FolkloreEntry> GetEntriesByContentType(string contentType);
    FolkloreEntry? GetEntryById(int id);
    FolkloreEntry? GetEntryBySlug(string slug);
    IReadOnlyList<FolkloreEntry> GetEntriesByRegion(int regionId);
    IReadOnlyList<FolkloreEntry> GetEntriesByFestival(int festivalId);
    IReadOnlyList<FolkloreEntry> GetRelatedEntries(int entryId, int take = 4);
    IReadOnlyList<EntryFaq> GetFaqsForEntry(int entryId);
    IReadOnlyList<SourceEvidence> GetSourcesForEntry(int entryId);
    StructuredKnowledgeEntry? GetStructuredEntry(string slug);

    IReadOnlyList<SubmissionRecord> GetSubmissions();
    SubmissionRecord? GetSubmissionById(int id);
    int GetPendingSubmissionCount();
    int CreateSubmission(SubmissionInput input);
    void UpdateSubmissionStatus(int submissionId, SubmissionStatus status, string? reviewerNote, string reviewerName);
    IReadOnlyList<ReviewHistory> GetReviewHistory(int submissionId);
    void AddReviewHistory(ReviewHistory history);
    void BulkUpdateSubmissionStatus(List<int> submissionIds, SubmissionStatus status, string? reviewerNote, string reviewerName);
}
