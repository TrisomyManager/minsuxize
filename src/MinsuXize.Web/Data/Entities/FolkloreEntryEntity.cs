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
}
