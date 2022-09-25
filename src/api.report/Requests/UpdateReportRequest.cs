namespace api.report.Requests;

public class UpdateReportRequest
{
    public Guid Id { get; set; }

    public int StatusId { get; set; }

    public string FilePath { get; set; } = default!;
}