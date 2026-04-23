using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using MinsuXize.Web.Models;

namespace MinsuXize.Web.ViewModels;

public sealed class SubmitEntryViewModel
{
    public int? RelatedEntryId { get; set; }
    public string? RelatedEntryTitle { get; set; }

    [Display(Name = "内容类型")]
    public string ContentType { get; set; } = "ritual";

    [Required(ErrorMessage = "请填写反馈人")]
    [Display(Name = "反馈人")]
    public string ContributorName { get; set; } = string.Empty;

    [Required(ErrorMessage = "请选择地区")]
    [Display(Name = "地区")]
    public int? RegionId { get; set; }

    [Required(ErrorMessage = "请选择节日或时间节点")]
    [Display(Name = "节日或时间节点")]
    public int? FestivalId { get; set; }

    [Required(ErrorMessage = "请填写标题")]
    [StringLength(100)]
    [Display(Name = "标题")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "请填写内容摘要")]
    [StringLength(1200)]
    [Display(Name = "内容摘要")]
    public string Summary { get; set; } = string.Empty;

    [Required(ErrorMessage = "请填写来源说明")]
    [StringLength(1200)]
    [Display(Name = "来源说明")]
    public string SourceSummary { get; set; } = string.Empty;

    [Display(Name = "联系方式")]
    [StringLength(120)]
    public string? Contact { get; set; }

    [Display(Name = "图片链接")]
    [StringLength(2000)]
    public string? ImagesInput { get; set; }

    [Display(Name = "视频链接")]
    [StringLength(2000)]
    public string? VideosInput { get; set; }

    [Display(Name = "音频链接")]
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

    [Display(Name = "本次补充说明")]
    [StringLength(500)]
    public string? ChangeLog { get; set; }

    public IReadOnlyList<SelectListItem> Regions { get; set; } = [];
    public IReadOnlyList<SelectListItem> Festivals { get; set; } = [];
    public IReadOnlyList<SelectListItem> ContentTypes { get; set; } = [];

    public List<string> GetImagesList() => SplitLinks(ImagesInput);
    public List<string> GetVideosList() => SplitLinks(VideosInput);
    public List<string> GetAudiosList() => SplitLinks(AudiosInput);

    public LocationInfo? GetLocationInfo()
    {
        if (Latitude.HasValue && Longitude.HasValue)
        {
            return new LocationInfo
            {
                Latitude = Latitude.Value,
                Longitude = Longitude.Value,
                Address = Address,
                Description = string.IsNullOrWhiteSpace(Address) ? "用户补充的位置线索" : $"用户补充的位置线索：{Address}"
            };
        }

        return null;
    }

    private static List<string> SplitLinks(string? input) =>
        string.IsNullOrWhiteSpace(input)
            ? []
            : input.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Where(value => !string.IsNullOrWhiteSpace(value))
                .ToList();
}
