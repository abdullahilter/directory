using entity.common;

namespace entity.contact;

public class Communication : IEntity
{
    public Guid Id { get; set; }

    public Guid ContactId { get; set; }

    public int TypeId { get; set; }

    public string Content { get; set; } = default!;
}