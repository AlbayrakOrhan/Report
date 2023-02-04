using Report.Domain.Entities;

namespace Report.Application.Interfaces;

public interface IReportRequestAssembler
{
    ReportRequest CreateNewReportRequestEntity();
}