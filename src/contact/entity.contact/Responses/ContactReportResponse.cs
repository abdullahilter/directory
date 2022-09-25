namespace entity.contact;

public class ContactReportResponse
{
    public string Location { get; set; } = default!;

    public int ContactCountInThatLocation { get; set; } = 0;

    public int TelephoneNumberCountInThatLocation { get; set; } = 0;
}