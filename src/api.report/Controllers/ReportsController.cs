using api.report.Requests;
using api.report.Services;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IResult> CreateAsync(CancellationToken cancellationToken)
    {
        await _service.CreateAsync(new CreateReportRequest(), cancellationToken);

        return Results.Accepted();
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
}