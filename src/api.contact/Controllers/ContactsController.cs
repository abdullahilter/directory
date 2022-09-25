using api.contact.Requests;
using api.contact.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.contact.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactsController : ControllerBase
{
    private readonly IContactService _service;

    public ContactsController(IContactService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IResult> CreateAsync(CreateContactRequest model, CancellationToken cancellationToken)
    {
        var result = await _service.CreateAsync(model, cancellationToken);

        if (result is null) return Results.NotFound();

        return Results.Created($"/contacts/{result.Id}", result);
    }

    [HttpGet]
    public async Task<IResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var results = await _service.GetAllAsync(cancellationToken);

        return Results.Ok(results);
    }

    [HttpGet("{id:guid}")]
    public async Task<IResult> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _service.GetAsync(id, cancellationToken);

        if (result is null) return Results.NotFound();

        return Results.Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(id, cancellationToken);

        return Results.NoContent();
    }
}