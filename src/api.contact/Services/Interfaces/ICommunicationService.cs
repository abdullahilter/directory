using api.contact.Entities;
using api.contact.Requests;
using common.Refit;

namespace api.contact.Services;

public interface ICommunicationService
{
    Task<Communication> CreateAsync(CreateCommunicationRequest request, CancellationToken cancellationToken);

    Task<List<ContactReportResponse>> GetReportAsync(CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}