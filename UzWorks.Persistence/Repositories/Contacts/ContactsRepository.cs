using Microsoft.EntityFrameworkCore;
using UzWorks.Core.Entities.Contacts;
using UzWorks.Persistence.Data;

namespace UzWorks.Persistence.Repositories.Contacts;

public class ContactsRepository : GenericRepository<Contact>, IContactsRepository 
{
    public ContactsRepository(UzWorksDbContext context) : base(context)
    {
    }

    public async Task<Contact[]> GetAllContactsAsync(int pageNumber, int pageSize, bool? isComplated)
    {
        var query = _dbSet.Where(j => !j.IsDeleted).AsQueryable();

        if (isComplated != null)
            query = query.Where(x => x.IsComplated == isComplated);

        if (pageNumber != 0 && pageSize != 0)
            query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);

        return await query.ToArrayAsync();
    }
}
