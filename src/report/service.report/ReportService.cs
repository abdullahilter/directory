using entity.common;
using entity.report;
using Mapster;
using MassTransit;
using service.common;

namespace service.report;

public class ReportService : IReportService
{
    private readonly IRepository<Report> _repository;
    private readonly IPublishEndpoint _publishEndpoint;

    public ReportService(IRepository<Report> repository, IPublishEndpoint publishEndpoint)
    {
        _repository = repository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Report> CreateAsync(CreateReportRequest request, CancellationToken cancellationToken)
    {
        var entity = request.Adapt<Report>();

        await _repository.CreateAsync(entity, cancellationToken);

        await _publishEndpoint.Publish(new ReportRequestCreated(entity.Id), cancellationToken);

        return entity;
    }

    public async Task<IReadOnlyCollection<ReportResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        var reports = await _repository.GetAllAsync(cancellationToken);

        var results = reports.Select(s => new ReportResponse
        {
            Id = s.Id,
            RequestedDate = s.RequestedDate,
            FilePath = s.FilePath,
            StatusId = s.StatusId
        }).ToList();

        return results;
    }

    public async Task<Report> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAsync(id, cancellationToken);

        return result;
    }

    public async Task UpdateAsync(UpdateReportRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetAsync(request.Id, cancellationToken);
        ArgumentNullException.ThrowIfNull(entity);

        entity = request.Adapt(entity);

        await _repository.UpdateAsync(entity, cancellationToken);
    }
}