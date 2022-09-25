using entity.common;

namespace entity.contact;

public class Contact : IEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public string Surname { get; set; } = default!;

    public string CompanyName { get; set; } = default!;
}