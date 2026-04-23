namespace MinsuXize.Web.Data.Entities;

public sealed class FestivalEntity
{
    public int Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string LunarLabel { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string CoreTopicsJson { get; set; } = "[]";
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
