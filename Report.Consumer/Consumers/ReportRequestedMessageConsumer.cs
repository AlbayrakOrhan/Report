using MassTransit;
using Report.Application.Interfaces;
using Report.Domain.Enums;
using Report.Domain.Messages;
using Report.Infrastructure.ContactService;

namespace Report.Consumer.Consumers;

public class ReportRequestedMessageConsumer : IConsumer<ReportRequestMessage>
{
    private readonly IContactServiceCommunicator _contactServiceCommunicator;
    private readonly IReportRequestRepository _reportRequestRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReportRequestedMessageConsumer(IContactServiceCommunicator contactServiceCommunicator, IReportRequestRepository reportRequestRepository, IUnitOfWork unitOfWork)
    {
        _contactServiceCommunicator = contactServiceCommunicator;
        _reportRequestRepository = reportRequestRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Consume(ConsumeContext<ReportRequestMessage> context)
    {
        var report = await _contactServiceCommunicator.GetReportAsync();
        if (report == null)
        {
            throw new Exception("Report data not found");
        }

        // excel file create
        // get file path
        var filePath = "../filePath.xslt";
        // ...

        var existingReportRequest = await _reportRequestRepository.GetById(context.Message.ReportRequestId);
        existingReportRequest.Set(filePath, DateTime.Now, ReportStatus.Completed);
        
        _reportRequestRepository.Update(existingReportRequest);
        await _unitOfWork.SaveChangesAsync();
    }
}