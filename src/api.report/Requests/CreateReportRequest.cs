using api.report.Enums;

namespace api.report.Requests;

public class CreateReportRequest
{
    public int StatusId { get; set; } = (int)ReportStatus.Preparing;

    public DateTime RequestedDate { get; set; } = DateTime.UtcNow;
}