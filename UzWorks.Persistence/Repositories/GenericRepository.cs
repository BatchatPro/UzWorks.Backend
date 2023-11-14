using Microsoft.EntityFrameworkCore;
using UzWorks.Core.Entities;
using UzWorks.Persistence.Data;

namespace UzWorks.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly DbSet<T> _dbSet;
    protected readonly UzWorksDbContext _context;

    protected GenericRepository(UzWorksDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public virtual ValueTask<T?> GetById(Guid id)
    {
        return _dbSet.FindAsync(id);
    }

    public virtual async Task<T> CreateAsync(T entity)
    {
        if(entity.Equals(Guid.Empty))
            entity.Id = Guid.NewGuid();

        entity.CreateDate = DateTime.Now;
        entity.UpdateDate = DateTime.Now;

        await _dbSet.AddAsync(entity);

        return entity;
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void UpdateAsync(T entity)
    {
        entity.UpdateDate = DateTime.Now;
        _context.Entry(entity).State = EntityState.Modified;
    }

    public async Task<int> SaveChanges()
    {
        return await _context.SaveChangesAsync();
    }
}
