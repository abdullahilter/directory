namespace entity.report;

public class UpdateReportRequest
{
    public Guid Id { get; set; }

    public int StatusId { get; set; }

    public string FilePath { get; set; } = default!;
}