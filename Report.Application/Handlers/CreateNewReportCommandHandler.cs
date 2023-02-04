using MediatR;
using Report.Application.Commands.CreateNewReport;
using Report.Application.Interfaces;

namespace Report.Application.Handlers;

public class CreateNewReportCommandHandler : IRequestHandler<CreateNewReportCommand, CreateNewReportCommandResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReportRequestRepository _reportRequestRepository;
    private readonly IReportRequestAssembler _reportRequestAssembler;

    public CreateNewReportCommandHandler(IUnitOfWork unitOfWork, IReportRequestRepository reportRequestRepository, IReportRequestAssembler reportRequestAssembler)
    {
        _unitOfWork = unitOfWork;
        _reportRequestRepository = reportRequestRepository;
        _reportRequestAssembler = reportRequestAssembler;
    }

    public async Task<CreateNewReportCommandResult> Handle(CreateNewReportCommand request, CancellationToken cancellationToken)
    {
        var newReportRequest = _reportRequestAssembler.CreateNewReportRequestEntity();
        newReportRequest.AddPaymentRequestCreatedEvent();

        await _reportRequestRepository.Save(newReportRequest);
        await _unitOfWork.SaveChangesAsync();

        return new CreateNewReportCommandResult();
    }
}