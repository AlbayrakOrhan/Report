using Report.Infrastructure.ContactService.Models.Response;

namespace Report.Infrastructure.ContactService;

public interface IContactServiceCommunicator
{
    Task<ReportDataResponse?> GetReportAsync();
}