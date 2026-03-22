using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

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

    public IReadOnlyList<SelectListItem> Regions { get; set; } = [];
    public IReadOnlyList<SelectListItem> Festivals { get; set; } = [];
}
