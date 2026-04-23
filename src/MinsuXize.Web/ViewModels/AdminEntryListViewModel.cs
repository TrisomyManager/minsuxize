using MinsuXize.Web.Models;

namespace MinsuXize.Web.ViewModels;

public sealed class AdminEntryListViewModel
{
    public required IReadOnlyList<FolkloreEntry> Entries { get; init; }
    public required IReadOnlyDictionary<int, Region> RegionsById { get; init; }
    public required IReadOnlyDictionary<int, Festival> FestivalsById { get; init; }
}
