using Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public interface IBaseRepo<TEntity> where TEntity : class
{
    Task<bool> AddAsync(TEntity entity);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression);
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression);
    Task<bool> UpdateAsync(TEntity entity);
}

public class BaseRepo<TEntity> : IBaseRepo<TEntity> where TEntity : class
{
    protected readonly DataContext _context;
    protected readonly DbSet<TEntity> _table;

    public BaseRepo(DataContext context)
    {
        _context = context;
        _table = _context.Set<TEntity>();
    }

    public virtual async Task<bool> AddAsync(TEntity entity)
    {
        if (entity == null)
            return false;

        try
        {
            await _table.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var entities = await _table.ToListAsync();
        return entities;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entities = await _table.Where(expression).ToListAsync();
        return entities;
    }

    public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await _table.FirstOrDefaultAsync(expression);
        return entity;
    }

    public virtual async Task<bool> UpdateAsync(TEntity entity)
    {
        if (entity == null)
            return false;

        try
        {
            _table.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
