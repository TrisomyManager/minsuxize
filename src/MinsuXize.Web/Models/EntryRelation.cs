namespace MinsuXize.Web.Models;

public sealed class EntryRelation
{
    public required int Id { get; init; }
    public required int EntryId { get; init; }
    public required int RelatedEntryId { get; init; }
    public required string RelationType { get; init; }
    public required string Note { get; init; }
}
