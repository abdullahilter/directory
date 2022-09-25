namespace entity.common;

public class ContactReportResponse
{
    public string Location { get; set; } = default!;

    public int ContactCountInThatLocation { get; set; } = 0;

    public int CommunicationCountInThatLocation { get; set; } = 0;
}