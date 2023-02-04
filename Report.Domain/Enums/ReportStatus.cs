using System.ComponentModel;

namespace Report.Domain.Enums;

public enum ReportStatus
{
    [Description("In progress")]
    InProgress = 1,
    [Description("Completed")]
    Completed
}