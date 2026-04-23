using Microsoft.AspNetCore.Mvc;
using MinsuXize.Web.Services;

namespace MinsuXize.Web.Controllers;

[ApiController]
[Route("api/entries")]
public sealed class KnowledgeApiController : ControllerBase
{
    private readonly IFolkloreRepository _repository;

    public KnowledgeApiController(IFolkloreRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{slug}.json")]
    [HttpGet("{slug}")]
    public IActionResult Details(string slug)
    {
        var entry = _repository.GetStructuredEntry(slug);
        return entry is null ? NotFound() : Ok(entry);
    }
}
