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
    public async Task<IActionResult> CreateAsync(CreateContactRequest model, CancellationToken cancellationToken)
    {
        var result = await _service.CreateAsync(model, cancellationToken);

        if (result is null) return NotFound();

        return Created($"/contacts/{result.Id}", result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var results = await _service.GetAllAsync(cancellationToken);

        return Ok(results);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _service.GetAsync(id, cancellationToken);

        if (result is null) return NotFound();

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(id, cancellationToken);

        return NoContent();
    }
}