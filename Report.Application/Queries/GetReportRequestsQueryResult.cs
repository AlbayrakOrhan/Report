using Report.Application.Dtos;

namespace Report.Application.Queries;

public class GetReportRequestsQueryResult
{
    public List<ReportRequestDto> ReportRequests { get; set; }
}