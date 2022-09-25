namespace entity.report;

public class CreateReportRequest
{
    public int StatusId { get; set; } = (int)ReportStatus.Preparing;

    public DateTime RequestedDate { get; set; } = DateTime.UtcNow;
}