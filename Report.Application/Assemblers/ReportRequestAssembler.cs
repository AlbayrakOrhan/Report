using Report.Application.Interfaces;
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
}