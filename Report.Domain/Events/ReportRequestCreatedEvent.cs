using MediatR;

namespace Report.Domain.Events;

public class ReportRequestCreatedEvent : INotification
{
    public ReportRequestCreatedEvent(Guid reportId)
    {
        ReportId = reportId;
    }

    public Guid ReportId { get; set; }
}