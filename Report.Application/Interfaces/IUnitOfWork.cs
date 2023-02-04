namespace Report.Application.Interfaces;

public interface IUnitOfWork
{
    void SaveChanges();
    Task<int> SaveChangesAsync();
}