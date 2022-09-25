using api.report.Enums;
using Microsoft.OpenApi.Extensions;

namespace api.report.Response;

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
                StatusName = ((ReportStatus)_statusId).GetDisplayName();
        }
    }

    public string StatusName { get; set; } = default!;
}