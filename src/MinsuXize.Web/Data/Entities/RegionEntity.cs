namespace MinsuXize.Web.Data.Entities;

public sealed class RegionEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int? ParentId { get; set; }
    public string FullPath { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string CulturalFocus { get; set; } = string.Empty;
    public string HighlightsJson { get; set; } = "[]";
}
