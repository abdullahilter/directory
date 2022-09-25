using api.contact.Entities;
using api.contact.Requests;
using api.contact.Responses;
using common;
using Mapster;

namespace api.contact.Services;

public class ContactService : IContactService
{
    private readonly IRepository<Contact> _repository;
    private readonly IRepository<Communication> _communicationRepository;

    public ContactService(IRepository<Contact> repository, IRepository<Communication> communicationRepository)
    {
        _repository = repository;
        _communicationRepository = communicationRepository;
    }

    public async Task<Contact> CreateAsync(CreateContactRequest request, CancellationToken cancellationToken)
    {
        var entity = request.Adapt<Contact>();

        await _repository.CreateAsync(entity, cancellationToken);

        return entity;
    }

    public async Task<IReadOnlyCollection<Contact>> GetAllAsync(CancellationToken cancellationToken)
    {
        var results = await _repository.GetAllAsync(cancellationToken);

        return results;
    }

    public async Task<ContactDetailResponse> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var contact = await _repository.GetAsync(id, cancellationToken);
        ArgumentNullException.ThrowIfNull(contact);

        var communications = await _communicationRepository.GetAllAsync(x => x.ContactId == id, cancellationToken);

        var result = contact.Adapt<ContactDetailResponse>();
        ArgumentNullException.ThrowIfNull(result);

        if (communications is not null)
            result.Communications = communications
                .Select(s => new ContactCommunicationDto
                {
                    Id = s.Id,
                    TypeId = s.TypeId,
                    Content = s.Content
                }).ToList();

        return result;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(id, cancellationToken);
    }
}