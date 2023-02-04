using Report.Application.Dtos;
using Report.Domain.Entities;

namespace Report.Application.Interfaces;

public interface IReportRequestRepository : IGenericRepository<ReportRequest>
{
    Task<List<ReportRequestDto>> ListReportRequestsAsync(CancellationToken cancellationToken = default);
}