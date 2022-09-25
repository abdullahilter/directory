using api.contact.Entities;
using api.contact.Requests;
using api.contact.Responses;
using common.Refit;

namespace api.contact.Services;

public interface IContactService
{
    Task<Contact> CreateAsync(CreateContactRequest request, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<Contact>> GetAllAsync(CancellationToken cancellationToken);

    Task<ContactDetailResponse> GetAsync(Guid id, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}