using entity.common;
using entity.report;
using MassTransit;
using service.report;

namespace api.report.Consumers;

public class ReportRequestCreatedConsumer : IConsumer<ReportRequestCreated>
{
    private readonly IContactApi _contactApi;
    private readonly IFileService _fileService;
    private readonly IReportService _reportService;

    public ReportRequestCreatedConsumer(IContactApi contactApi, IFileService fileService, IReportService reportService)
    {
        _contactApi = contactApi;
        _fileService = fileService;
        _reportService = reportService;
    }

    public async Task Consume(ConsumeContext<ReportRequestCreated> context)
    {
        var data = await _contactApi.GetReportAsync();

        var filePath = _fileService.GenerateExcel(data);

        var request = new UpdateReportRequest
        {
            Id = context.Message.ReportId,
            StatusId = (int)ReportStatus.Completed,
            FilePath = filePath
        };

        await _reportService.UpdateAsync(request, context.CancellationToken);
    }
}