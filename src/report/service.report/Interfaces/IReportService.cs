using entity.report;

namespace service.report;

public interface IReportService
{
    Task<Report> CreateAsync(CreateReportRequest request, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<ReportResponse>> GetAllAsync(CancellationToken cancellationToken);

    Task<Report> GetAsync(Guid id, CancellationToken cancellationToken);

    Task UpdateAsync(UpdateReportRequest request, CancellationToken cancellationToken);
}