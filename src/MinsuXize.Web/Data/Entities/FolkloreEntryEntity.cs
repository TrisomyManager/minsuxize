namespace MinsuXize.Web.Data.Entities;

public sealed class FolkloreEntryEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int RegionId { get; set; }
    public int FestivalId { get; set; }
    public string Summary { get; set; } = string.Empty;
    public string HistoricalOrigin { get; set; } = string.Empty;
    public string SymbolicMeaning { get; set; } = string.Empty;
    public string InheritanceStatus { get; set; } = string.Empty;
    public string ChangeNotes { get; set; } = string.Empty;
    public string OralText { get; set; } = string.Empty;
    public string RitualProcessJson { get; set; } = "[]";
    public string ItemsUsedJson { get; set; } = "[]";
    public string TaboosJson { get; set; } = "[]";
    public string ParticipantsJson { get; set; } = "[]";
    
    // 新增字段
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; } = "system";
    public string Status { get; set; } = "draft"; // draft, submitted, reviewing, approved, published, archived
    public int Version { get; set; } = 1;
    public string ChangeLog { get; set; } = "Initial version";
    public string ImagesJson { get; set; } = "[]";
    public string VideosJson { get; set; } = "[]";
    public string AudiosJson { get; set; } = "[]";
    public string? LocationJson { get; set; }
}
