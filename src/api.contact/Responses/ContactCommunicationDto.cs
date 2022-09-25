using api.contact.Enums;
using Microsoft.OpenApi.Extensions;

namespace api.contact.Responses;

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
                TypeName = ((CommunicationType)_typeId).GetDisplayName();
        }
    }

    public string TypeName { get; set; } = default!;

    public string Content { get; set; } = default!;
}