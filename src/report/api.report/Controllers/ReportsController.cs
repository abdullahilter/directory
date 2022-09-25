using entity.report;
using Microsoft.AspNetCore.Mvc;
using service.report;

namespace api.report.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IReportService _service;

    public ReportsController(IReportService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CancellationToken cancellationToken)
    {
        await _service.CreateAsync(new CreateReportRequest(), cancellationToken);

        return Accepted();
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
}