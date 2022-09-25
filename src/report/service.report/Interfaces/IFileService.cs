using entity.report;

namespace service.report;

public interface IFileService
{
    string GenerateExcel(List<ContactReportResponse> data);
}