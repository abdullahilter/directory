using entity.contact;

namespace service.contact;

public interface ICommunicationService
{
    Task<Communication> CreateAsync(CreateCommunicationRequest request, CancellationToken cancellationToken);

    Task<List<ContactReportResponse>> GetReportAsync(CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}