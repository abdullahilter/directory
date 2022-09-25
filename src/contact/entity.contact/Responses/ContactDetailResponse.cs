namespace entity.contact;

public class ContactDetailResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public string Surname { get; set; } = default!;

    public string CompanyName { get; set; } = default!;

    public List<ContactCommunicationDto>? Communications { get; set; }
}