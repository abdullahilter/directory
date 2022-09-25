using entity.contact;
using Mapster;
using service.common;

namespace service.contact;

public class CommunicationService : ICommunicationService
{
    private readonly IRepository<Communication> _repository;
    private readonly IRepository<Contact> _contactRepository;

    public CommunicationService(IRepository<Communication> repository, IRepository<Contact> contactRepository)
    {
        _repository = repository;
        _contactRepository = contactRepository;
    }

    public async Task<Communication> CreateAsync(CreateCommunicationRequest request, CancellationToken cancellationToken)
    {
        var entity = request.Adapt<Communication>();

        var contact = await _contactRepository.GetAsync(entity.ContactId, cancellationToken);
        ArgumentNullException.ThrowIfNull(contact);

        await _repository.CreateAsync(entity, cancellationToken);

        return entity;
    }

    public async Task<List<ContactReportResponse>> GetReportAsync(CancellationToken cancellationToken)
    {
        var communications = await _repository.GetAllAsync(x => x.TypeId == (int)CommunicationType.Location, cancellationToken);

        var result = (
            from com in communications
            group com by com.Content into g
            select new ContactReportResponse
            {
                Location = g.Key,
                ContactCountInThatLocation = g.DistinctBy(d => d.ContactId).Count(),
                CommunicationCountInThatLocation = g.Count()
            }).ToList();

        return result ?? new List<ContactReportResponse>();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(id, cancellationToken);
    }
}