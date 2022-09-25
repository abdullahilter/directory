using api.contact.Requests;
using api.contact.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.contact.Controllers;

[ApiController]
[Route("[controller]")]
public class CommunicationsController : ControllerBase
{
    private readonly ICommunicationService _service;

    public CommunicationsController(ICommunicationService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IResult> CreateAsync(CreateCommunicationRequest model, CancellationToken cancellationToken)
    {
        var result = await _service.CreateAsync(model, cancellationToken);

        if (result is null) return Results.NotFound();

        return Results.Created($"/communications/{result.Id}", result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(id, cancellationToken);

        return Results.NoContent();
    }
}