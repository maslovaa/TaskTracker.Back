using System.Linq.Expressions;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

/// <summary>
/// Репозиторий.
/// </summary>
/// <typeparam name="T">Тип репозитория.</typeparam>
/// <typeparam name="TId">Тип идентификатора.</typeparam>
public abstract class Repository<T, TId> : IRepository<T, TId> 
    where T : class, IEntity<TId>
    where TId : struct
{
    protected DataContext _context;
    
    public Repository(DataContext context)
    {
        _context = context;
    }
    
    /// <inheritdoc/>
    public virtual IQueryable<T> GetAll(bool noTracking = false)
    {
        return noTracking ? _context.Set<T>().AsNoTracking() : _context.Set<T>();
    }

    /// <inheritdoc/>
    public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken, bool noTracking = false)
    {
        return await GetAll().ToListAsync(cancellationToken);
    }
    
    /// <inheritdoc/>>
    public virtual TId Add(T entity)
    {
        try
        {
            var result = _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return result.Entity.Id;
        }
        catch (Exception e)
        {
            return default(TId);
        }
    }

    /// <inheritdoc/>>
    public virtual async Task<TId> AddAsync(T entity)
    {
        try
        {
            var result = _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return result.Entity.Id;
        }
        catch (Exception e)
        {
            return default(TId);
        }
    }

    /// <inheritdoc/>
    public virtual T GetById(TId id)
    {
        return _context.Set<T>().Find(id);
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetByIdAsync(TId id, CancellationToken cancellationToken)
    {
        return await _context.Set<T>().FindAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<IEnumerable<T>> GetByPredicate(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.Set<T>().Where(predicate).ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<bool> Update(T entity)
    {
        try
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    /// <inheritdoc/>
    public virtual async Task<bool> Delete(TId id, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<T>().FindAsync(id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken) ;
        return true;
    }

    /// <inheritdoc/>
    public virtual bool Delete(T entity)
    {
        if (entity is null)
        {
            return false;
        }

        _context.Set<T>().Entry(entity).State = EntityState.Deleted;
        return true;
    }
}