namespace MinsuXize.Web.Data.Entities;

public sealed class EntryFaqEntity
{
    public int Id { get; set; }
    public int EntryId { get; set; }
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}
