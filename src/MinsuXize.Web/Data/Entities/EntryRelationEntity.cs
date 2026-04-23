namespace MinsuXize.Web.Data.Entities;

public sealed class EntryRelationEntity
{
    public int Id { get; set; }
    public int EntryId { get; set; }
    public int RelatedEntryId { get; set; }
    public string RelationType { get; set; } = "related";
    public string Note { get; set; } = string.Empty;
}
