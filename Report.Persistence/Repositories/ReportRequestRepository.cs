using Microsoft.EntityFrameworkCore;
using Report.Application.Dtos;
using Report.Application.Interfaces;
using Report.Domain.Entities;
using Report.Persistence.Context;

namespace Report.Persistence.Repositories;

public class ReportRequestRepository : GenericRepository<ReportRequest>, IReportRequestRepository
{
    public ReportRequestRepository(ReportDbContext context) : base(context)
    {
    }

    public async Task<List<ReportRequestDto>> ListReportRequestsAsync(CancellationToken cancellationToken)
    {
        return await All().Select(x => new ReportRequestDto()
        {
            ReportPath = x.ReportPath,
            Status = x.Status,
            CompletedDate = x.CompletedDate
        }).ToListAsync(cancellationToken);
    }
}