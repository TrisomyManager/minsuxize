using MinsuXize.Web.Models;

namespace MinsuXize.Web.ViewModels;

public sealed class FestivalDetailsViewModel
{
    public required Festival Festival { get; init; }
    public required IReadOnlyList<FolkloreEntry> Entries { get; init; }
}
