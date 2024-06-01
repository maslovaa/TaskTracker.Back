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
    where T : class, IEntity<TId>, IIsActive
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
        return noTracking ? _context.Set<T>().Where(x => x.IsActive == true).AsNoTracking() : _context.Set<T>().Where(x => x.IsActive == true);
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
    public virtual async Task<IEnumerable<T>> GetByPredicateAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.Set<T>().Where(predicate).ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual IEnumerable<T> GetByPredicate(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().Where(predicate).ToList();
    }

    /// <inheritdoc/>
    public virtual async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        try
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    /// <inheritdoc/>
    public virtual bool Update(T entity)
    {
        try
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    /// <inheritdoc/>
    public virtual async Task<bool> DeleteAsync(TId id, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<T>().FindAsync(id, cancellationToken);
        if (entity is null)
        {
            return false;
        }
        entity.IsActive = false;
        await _context.SaveChangesAsync(cancellationToken) ;
        return true;
    }

    public virtual bool Delete(TId id)
    {
        var entity = _context.Set<T>().Find(id);
        if (entity is null)
        {
            return false;
        }
        entity.IsActive = false;
        _context.SaveChanges();
        return true;
    }
}