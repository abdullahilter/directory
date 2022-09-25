using entity.report;
using Refit;

namespace service.report;

[Headers("Content-Type: application/json")]
public interface IContactApi
{
    [Get("/reports")]
    Task<List<ContactReportResponse>> GetReportAsync();
}