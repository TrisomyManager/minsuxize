namespace MinsuXize.Web.Models;

public sealed class EntryFaq
{
    public required int Id { get; init; }
    public required int EntryId { get; init; }
    public required string Question { get; init; }
    public required string Answer { get; init; }
    public required int SortOrder { get; init; }
}
