using Report.Application.Interfaces;
using Report.Domain.Entities;
using Report.Persistence.Context;

namespace Report.Persistence.Repositories;

public class ReportRequestRepository : GenericRepository<ReportRequest>, IReportRequestRepository
{
    public ReportRequestRepository(ReportDbContext context) : base(context)
    {
    }
}