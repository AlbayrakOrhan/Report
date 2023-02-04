using MassTransit;
using MediatR;
using Report.Domain.Events;
using Report.Domain.Messages;

namespace Report.Application.Handlers;

public class ReportRequestCreatedEventHandler : INotificationHandler<ReportRequestCreatedEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public ReportRequestCreatedEventHandler(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public Task Handle(ReportRequestCreatedEvent notification, CancellationToken cancellationToken)
    {
        return _publishEndpoint.Publish(new ReportRequestMessage()
        {
            ReportRequestId = notification.ReportId
        }, cancellationToken);
    }
}