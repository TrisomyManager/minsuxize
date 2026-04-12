using System.Text.Json;

namespace MinsuXize.Web.Data.Entities;

public sealed class SubmissionRecordEntity
{
    public int Id { get; set; }
    public string ContributorName { get; set; } = string.Empty;
    public int RegionId { get; set; }
    public int FestivalId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string SourceSummary { get; set; } = string.Empty;
    public string? Contact { get; set; }
    public DateTime SubmittedAtUtc { get; set; }
    public int Status { get; set; }
    public string? ReviewerNote { get; set; }
    
    // 新增字段
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public int Version { get; set; } = 1;
    public string? ChangeLog { get; set; }
    public string ImagesJson { get; set; } = "[]";
    public string VideosJson { get; set; } = "[]";
    public string AudiosJson { get; set; } = "[]";
    public string? LocationJson { get; set; }
    
    // 便捷属性
    public List<string> Images
    {
        get => JsonSerializer.Deserialize<List<string>>(ImagesJson) ?? new List<string>();
        set => ImagesJson = JsonSerializer.Serialize(value);
    }
    
    public List<string> Videos
    {
        get => JsonSerializer.Deserialize<List<string>>(VideosJson) ?? new List<string>();
        set => VideosJson = JsonSerializer.Serialize(value);
    }
    
    public List<string> Audios
    {
        get => JsonSerializer.Deserialize<List<string>>(AudiosJson) ?? new List<string>();
        set => AudiosJson = JsonSerializer.Serialize(value);
    }
    
    public LocationInfoData? Location
    {
        get => string.IsNullOrEmpty(LocationJson) ? null : JsonSerializer.Deserialize<LocationInfoData>(LocationJson);
        set => LocationJson = value == null ? null : JsonSerializer.Serialize(value);
    }
}

// 使用不同的类名避免 EF Core 将其识别为实体
public class LocationInfoData
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Province { get; set; }
    public string? Country { get; set; }
    public string? Description { get; set; }
}
