namespace MinsuXize.Web.Data.Entities;

public sealed class FolkloreEntryEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string ContentType { get; set; } = "ritual";
    public int RegionId { get; set; }
    public int FestivalId { get; set; }
    public string Summary { get; set; } = string.Empty;
    public string RegionFieldsJson { get; set; } = "{}";
    public string HistoricalOrigin { get; set; } = string.Empty;
    public string SymbolicMeaning { get; set; } = string.Empty;
    public string InheritanceStatus { get; set; } = string.Empty;
    public string ChangeNotes { get; set; } = string.Empty;
    public string OralText { get; set; } = string.Empty;
    public string PreparationsJson { get; set; } = "[]";
    public string RitualProcessJson { get; set; } = "[]";
    public string RegionalDifferencesJson { get; set; } = "[]";
    public string ItemsUsedJson { get; set; } = "[]";
    public string TaboosJson { get; set; } = "[]";
    public string ParticipantsJson { get; set; } = "[]";
    public string TagsJson { get; set; } = "[]";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; } = "system";
    public string Status { get; set; } = "draft";
    public string ReviewStatus { get; set; } = "pending-verification";
    public string ConfidenceLevel { get; set; } = "medium";
    public string SourceGrade { get; set; } = "unverified";
    public int Version { get; set; } = 1;
    public string ChangeLog { get; set; } = "Initial version";
    public string ImagesJson { get; set; } = "[]";
    public string VideosJson { get; set; } = "[]";
    public string AudiosJson { get; set; } = "[]";
    public string? LocationJson { get; set; }
}
