using api.report.Entities;
using api.report.Requests;
using api.report.Response;

namespace api.report.Services;

public interface IReportService
{
    Task<Report> CreateAsync(CreateReportRequest request, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<ReportResponse>> GetAllAsync(CancellationToken cancellationToken);

    Task<Report> GetAsync(Guid id, CancellationToken cancellationToken);

    Task UpdateAsync(UpdateReportRequest request, CancellationToken cancellationToken);
}