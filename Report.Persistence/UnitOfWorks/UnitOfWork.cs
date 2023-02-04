using Report.Application.Interfaces;
using Report.Persistence.Context;

namespace Report.Persistence.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly ReportDbContext _dbContext;

    public UnitOfWork(ReportDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }

    public Task<int> SaveChangesAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
}