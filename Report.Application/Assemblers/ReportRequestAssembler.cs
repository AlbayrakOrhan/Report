using Report.Application.Dtos;
using Report.Application.Interfaces;
using Report.Application.Queries;
using Report.Domain.Entities;
using Report.Domain.Enums;

namespace Report.Application.Assemblers;

public class ReportRequestAssembler : IReportRequestAssembler
{
    public ReportRequest CreateNewReportRequestEntity()
    {
        return new ReportRequest()
        {
            Id = Guid.NewGuid(),
            Status = ReportStatus.InProgress
        };
    }

    public GetReportRequestsQueryResult MapToGetReportRequestsQueryResult(List<ReportRequestDto> reportRequestDtos)
    {
        return new GetReportRequestsQueryResult()
        {
            ReportRequests = reportRequestDtos
        };
    }
}