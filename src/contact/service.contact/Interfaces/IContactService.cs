using entity.contact;

namespace service.contact;

public interface IContactService
{
    Task<Contact> CreateAsync(CreateContactRequest request, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<Contact>> GetAllAsync(CancellationToken cancellationToken);

    Task<ContactDetailResponse> GetAsync(Guid id, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}