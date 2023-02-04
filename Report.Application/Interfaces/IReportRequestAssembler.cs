using Report.Application.Dtos;
using Report.Application.Queries;
using Report.Domain.Entities;

namespace Report.Application.Interfaces;

public interface IReportRequestAssembler
{
    ReportRequest CreateNewReportRequestEntity();
    GetReportRequestsQueryResult MapToGetReportRequestsQueryResult(List<ReportRequestDto> reportRequestDtos);
}