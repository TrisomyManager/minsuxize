namespace MinsuXize.Web.Models;

public sealed class FolkloreEntry
{
    public required int Id { get; init; }
    public required string Title { get; init; }
    public required int RegionId { get; init; }
    public required int FestivalId { get; init; }
    public required string Summary { get; init; }
    public required string HistoricalOrigin { get; init; }
    public required string SymbolicMeaning { get; init; }
    public required string InheritanceStatus { get; init; }
    public required string ChangeNotes { get; init; }
    public required string OralText { get; init; }
    public IReadOnlyList<string> RitualProcess { get; init; } = [];
    public IReadOnlyList<string> ItemsUsed { get; init; } = [];
    public IReadOnlyList<string> Taboos { get; init; } = [];
    public IReadOnlyList<string> Participants { get; init; } = [];
    public IReadOnlyList<int> SourceIds { get; init; } = [];
}
