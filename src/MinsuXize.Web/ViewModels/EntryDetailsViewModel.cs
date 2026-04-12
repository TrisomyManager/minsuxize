using MinsuXize.Web.Models;

namespace MinsuXize.Web.ViewModels;

public sealed class EntryDetailsViewModel
{
    public required FolkloreEntry Entry { get; init; }
    public required Region Region { get; init; }
    public required Festival Festival { get; init; }
    public required IReadOnlyList<SourceEvidence> Sources { get; init; }
}
