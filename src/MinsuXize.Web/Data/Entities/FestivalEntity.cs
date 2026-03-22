namespace MinsuXize.Web.Data.Entities;

public sealed class FestivalEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string LunarLabel { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string CoreTopicsJson { get; set; } = "[]";
}
