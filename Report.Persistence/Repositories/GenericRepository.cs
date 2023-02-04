using System.Linq.Expressions;
using Report.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Report.Domain.Abstracts;
using Report.Persistence.Context;

namespace Report.Persistence.Repositories;

public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : EntityBase
{
    private readonly ReportDbContext _context;
    private readonly DbSet<TEntity> _entities;

    public GenericRepository(ReportDbContext context)
    {
        _context = context;
        _entities = _context.Set<TEntity>();
    }

    public async Task<TEntity> GetById(Guid id)
    {
        return await _entities.SingleOrDefaultAsync(s => s.Id == id);
    }

    public IQueryable<TEntity> All()
    {
        return _entities.AsQueryable();
    }

    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
    {
        return _entities.Where(predicate);
    }

    public async Task<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
    {
        return await _entities.SingleOrDefaultAsync(predicate);
    }

    public async Task<IList<TEntity>> FilterBy(Expression<Func<TEntity, bool>> predicate)
    {
        return await _entities.Where(predicate).ToListAsync();
    }

    public virtual async Task<Guid> Save(TEntity entity)
    {
        var entityEntry = await _entities.AddAsync(entity);

        var id = entityEntry.Entity.Id;
        return id;
    }

    public virtual TEntity Update(TEntity entity)
    {
        var entityEntry = _entities.Update(entity);

        return entityEntry.Entity;
    }

    public async Task<int> Count(Expression<Func<TEntity, bool>> predicate = null)
    {
        var query = _entities.AsQueryable();
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return await query.CountAsync();
    }

    public async Task<bool> Any(Expression<Func<TEntity, bool>> predicate)
    {
        return await _entities.AnyAsync(predicate);
    }

    public Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        return _entities.AddRangeAsync(entities);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetById(id);
        _context.Entry(entity).State = EntityState.Deleted;
    }

    public void Delete(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException();
        }

        _entities.Remove(entity);
    }
}