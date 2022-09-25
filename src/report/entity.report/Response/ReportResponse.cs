namespace entity.report;

public class ReportResponse
{
    public Guid Id { get; set; }

    public DateTime RequestedDate { get; set; }

    public string? FilePath { get; set; }

    private int _statusId;
    public int StatusId
    {
        get { return _statusId; }
        set
        {
            _statusId = value;

            if (_statusId > 0 && _statusId < 3)
                StatusName = ((ReportStatus)_statusId).ToString();
        }
    }

    public string StatusName { get; set; } = default!;
}