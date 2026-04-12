namespace MinsuXize.Web.ViewModels;

public sealed class StatusPageViewModel
{
    public int StatusCode { get; init; }

    public string Eyebrow { get; init; } = string.Empty;

    public string Heading { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public string Guidance { get; init; } = string.Empty;
}
