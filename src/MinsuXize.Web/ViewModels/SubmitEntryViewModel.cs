using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using MinsuXize.Web.Models;

namespace MinsuXize.Web.ViewModels;

public sealed class SubmitEntryViewModel
{
    [Required(ErrorMessage = "请填写投稿人姓名")]
    [Display(Name = "投稿人")]
    public string ContributorName { get; set; } = string.Empty;

    [Required(ErrorMessage = "请选择地区")]
    [Display(Name = "地区")]
    public int? RegionId { get; set; }

    [Required(ErrorMessage = "请选择节日或时间节点")]
    [Display(Name = "节日或时间节点")]
    public int? FestivalId { get; set; }

    [Required(ErrorMessage = "请填写条目标题")]
    [StringLength(100)]
    [Display(Name = "条目标题")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "请填写民俗内容摘要")]
    [StringLength(1200)]
    [Display(Name = "民俗内容摘要")]
    public string Summary { get; set; } = string.Empty;

    [Required(ErrorMessage = "请填写来源说明")]
    [StringLength(1200)]
    [Display(Name = "来源说明")]
    public string SourceSummary { get; set; } = string.Empty;

    [Display(Name = "联系方式")]
    [StringLength(120)]
    public string? Contact { get; set; }

    // 新增字段
    [Display(Name = "图片链接（多个链接用逗号分隔）")]
    [StringLength(2000)]
    public string? ImagesInput { get; set; }

    [Display(Name = "视频链接（多个链接用逗号分隔）")]
    [StringLength(2000)]
    public string? VideosInput { get; set; }

    [Display(Name = "音频链接（多个链接用逗号分隔）")]
    [StringLength(2000)]
    public string? AudiosInput { get; set; }

    [Display(Name = "纬度")]
    [Range(-90, 90, ErrorMessage = "纬度必须在 -90 到 90 之间")]
    public double? Latitude { get; set; }

    [Display(Name = "经度")]
    [Range(-180, 180, ErrorMessage = "经度必须在 -180 到 180 之间")]
    public double? Longitude { get; set; }

    [Display(Name = "详细地址")]
    [StringLength(500)]
    public string? Address { get; set; }

    [Display(Name = "变更说明")]
    [StringLength(500)]
    public string? ChangeLog { get; set; }

    public IReadOnlyList<SelectListItem> Regions { get; set; } = [];
    public IReadOnlyList<SelectListItem> Festivals { get; set; } = [];

    // 便捷方法：将逗号分隔的字符串转换为列表
    public List<string> GetImagesList() => 
        string.IsNullOrWhiteSpace(ImagesInput) 
            ? new List<string>() 
            : ImagesInput.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();

    public List<string> GetVideosList() => 
        string.IsNullOrWhiteSpace(VideosInput) 
            ? new List<string>() 
            : VideosInput.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();

    public List<string> GetAudiosList() => 
        string.IsNullOrWhiteSpace(AudiosInput) 
            ? new List<string>() 
            : AudiosInput.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();

    public LocationInfo? GetLocationInfo()
    {
        if (Latitude.HasValue && Longitude.HasValue)
        {
            return new LocationInfo
            {
                Latitude = Latitude.Value,
                Longitude = Longitude.Value,
                Address = Address,
                Description = $"用户提交的位置：{Address}"
            };
        }
        return null;
    }
}
