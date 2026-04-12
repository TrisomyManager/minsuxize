using MinsuXize.Web.Models;

namespace MinsuXize.Web.Services;

public interface IFolkloreRepository
{
    IReadOnlyList<Region> GetRegions();
    Region? GetRegionById(int id);
    IReadOnlyList<Region> GetChildRegions(int parentId);
    IReadOnlyList<int> GetRegionTreeIds(int regionId);
    IReadOnlyList<Festival> GetFestivals();
    Festival? GetFestivalById(int id);
    IReadOnlyList<FolkloreEntry> GetEntries();
    FolkloreEntry? GetEntryById(int id);
    IReadOnlyList<FolkloreEntry> GetEntriesByRegion(int regionId);
    IReadOnlyList<FolkloreEntry> GetEntriesByFestival(int festivalId);
    IReadOnlyList<SourceEvidence> GetSourcesForEntry(int entryId);
    IReadOnlyList<SubmissionRecord> GetSubmissions();
    SubmissionRecord? GetSubmissionById(int id);
    int GetPendingSubmissionCount();
    int CreateSubmission(SubmissionInput input);
    void UpdateSubmissionStatus(int submissionId, SubmissionStatus status, string? reviewerNote, string reviewerName);
    IReadOnlyList<ReviewHistory> GetReviewHistory(int submissionId);
    void AddReviewHistory(ReviewHistory history);
    void BulkUpdateSubmissionStatus(List<int> submissionIds, SubmissionStatus status, string? reviewerNote, string reviewerName);
}
