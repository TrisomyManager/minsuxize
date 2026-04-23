using MinsuXize.Web.Models;

namespace MinsuXize.Web.ViewModels;

public sealed class HomePageViewModel
{
    public required int LocationCount { get; init; }
    public required int FestivalCount { get; init; }
    public required int RitualCount { get; init; }
    public required int EntryCount { get; init; }
    public required int FeedbackCount { get; init; }
    public required IReadOnlyList<Region> FeaturedLocations { get; init; }
    public required IReadOnlyList<Festival> FeaturedFestivals { get; init; }
    public required IReadOnlyList<FolkloreEntry> LatestEntries { get; init; }
    public required IReadOnlyList<FolkloreEntry> PopularEntries { get; init; }
    public required IReadOnlyList<string> PopularTerms { get; init; }
}
