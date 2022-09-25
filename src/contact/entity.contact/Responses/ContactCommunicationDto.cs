namespace entity.contact;

public class ContactCommunicationDto
{
    public Guid Id { get; set; }

    private int _typeId;
    public int TypeId
    {
        get { return _typeId; }
        set
        {
            _typeId = value;

            if (_typeId > 0 && _typeId < 4)
                TypeName = ((CommunicationType)_typeId).ToString();
        }
    }

    public string TypeName { get; set; } = default!;

    public string Content { get; set; } = default!;
}