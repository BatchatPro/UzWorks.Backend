﻿using UzWorks.Core.Entities.Contacts;

namespace UzWorks.Persistence.Repositories.Contacts;

public interface IContactsRepository : IGenericRepository<Contact>
{
    Task<Contact[]> GetAllAsync(int pageNumber = 1, int pageSize = 15, bool? isComplated = null);
}
