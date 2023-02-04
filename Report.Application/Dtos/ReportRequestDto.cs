using Report.Domain.Enums;
using Report.Domain.Extensions;

namespace Report.Application.Dtos;

public class ReportRequestDto
{
    public string ReportPath { get; set; }
    public DateTime? CompletedDate { get; set; }
    public ReportStatus Status { get; set; }
    public string StatusDescription => Status.GetDescription();
}