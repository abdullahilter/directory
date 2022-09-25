using common.Refit;

namespace api.report.Services;

public interface IFileService
{
    string GenerateExcel(List<ContactReportResponse> data);
}