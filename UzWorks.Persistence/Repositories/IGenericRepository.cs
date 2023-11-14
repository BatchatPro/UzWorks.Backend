using UzWorks.Core.Entities;

namespace UzWorks.Persistence.Repositories;

public interface IGenericRepository<T> where T : BaseEntity
{
    ValueTask<T?> GetById(Guid id);
    
    Task<T> CreateAsync(T entity);

    void UpdateAsync(T entity);

    void Delete(T entity);

    Task<int> SaveChanges();
}
