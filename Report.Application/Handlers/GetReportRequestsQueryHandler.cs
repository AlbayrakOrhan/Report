using MediatR;
using Report.Application.Interfaces;
using Report.Application.Queries;

namespace Report.Application.Handlers;

public class GetReportRequestsQueryHandler : IRequestHandler<GetReportRequestsQuery, GetReportRequestsQueryResult>
{
    private readonly IReportRequestRepository _reportRequestRepository;
    private readonly IReportRequestAssembler _reportRequestAssembler;

    public GetReportRequestsQueryHandler(IReportRequestRepository reportRequestRepository, IReportRequestAssembler reportRequestAssembler)
    {
        _reportRequestRepository = reportRequestRepository;
        _reportRequestAssembler = reportRequestAssembler;
    }

    public async Task<GetReportRequestsQueryResult> Handle(GetReportRequestsQuery request, CancellationToken cancellationToken)
    {
        var reportRequests = await _reportRequestRepository.ListReportRequestsAsync(cancellationToken);
        return _reportRequestAssembler.MapToGetReportRequestsQueryResult(reportRequests);
    }
}