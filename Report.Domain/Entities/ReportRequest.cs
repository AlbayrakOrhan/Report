using Report.Domain.Abstracts;
using Report.Domain.Enums;

namespace Report.Domain.Entities;

public class ReportRequest : EntityBase
{
    public string ReportPath { get; set; }
    public DateTime? CompletedDate { get; set; }
    public ReportStatus Status { get; set; }

    public void Set(string reportPath, DateTime completedDate, ReportStatus status)
    {
        ReportPath = reportPath;
        CompletedDate = completedDate;
        Status = status;
    }
}