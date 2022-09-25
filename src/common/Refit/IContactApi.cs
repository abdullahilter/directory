using Refit;

namespace common.Refit;

[Headers("Content-Type: application/json")]
public interface IContactApi
{
    [Get("/reports")]
    Task<List<ContactReportResponse>> GetReportAsync();
}