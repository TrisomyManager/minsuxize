using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MinsuXize.Web.ViewModels;

public sealed class AdminEntryEditViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(160)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(180)]
    public string Slug { get; set; } = string.Empty;

    [Required]
    public string ContentType { get; set; } = "ritual";

    [Required]
    public int RegionId { get; set; }

    [Required]
    public int FestivalId { get; set; }

    [Required]
    [StringLength(1200)]
    public string Summary { get; set; } = string.Empty;

    public string RegionFieldsJson { get; set; } = "{}";
    public string MaterialsText { get; set; } = string.Empty;
    public string PreparationsText { get; set; } = string.Empty;
    public string StepsText { get; set; } = string.Empty;
    public string TaboosText { get; set; } = string.Empty;
    public string RegionalDifferencesText { get; set; } = string.Empty;
    public string ParticipantsText { get; set; } = string.Empty;
    public string TagsText { get; set; } = string.Empty;
    public string HistoricalOrigin { get; set; } = string.Empty;
    public string SymbolicMeaning { get; set; } = string.Empty;
    public string InheritanceStatus { get; set; } = string.Empty;
    public string ChangeNotes { get; set; } = string.Empty;
    public string OralText { get; set; } = string.Empty;
    public string Status { get; set; } = "published";
    public string ReviewStatus { get; set; } = "pending-verification";
    public string ConfidenceLevel { get; set; } = "medium";
    public string SourceGrade { get; set; } = "unverified";
    public string ChangeLog { get; set; } = string.Empty;
    public string FaqLines { get; set; } = string.Empty;
    public string RelatedEntrySlugs { get; set; } = string.Empty;

    public IReadOnlyList<SelectListItem> RegionOptions { get; set; } = [];
    public IReadOnlyList<SelectListItem> FestivalOptions { get; set; } = [];
    public IReadOnlyList<SelectListItem> ContentTypeOptions { get; set; } = [];
    public IReadOnlyList<SelectListItem> ReviewStatusOptions { get; set; } = [];
    public IReadOnlyList<SelectListItem> ConfidenceLevelOptions { get; set; } = [];
    public IReadOnlyList<SelectListItem> SourceGradeOptions { get; set; } = [];
}
