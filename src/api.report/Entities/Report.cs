using common;

namespace api.report.Entities;

public class Report : IEntity
{
    public Guid Id { get; set; }

    public DateTime RequestedDate { get; set; }

    public string? FilePath { get; set; }

    public int StatusId { get; set; }
}