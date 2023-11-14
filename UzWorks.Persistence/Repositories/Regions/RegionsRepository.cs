﻿using Microsoft.EntityFrameworkCore;
using UzWorks.Core.Entities.Location;
using UzWorks.Persistence.Data;

namespace UzWorks.Persistence.Repositories.Regions;

public class RegionsRepository : GenericRepository<Region>, IRegionsRepository
{
    public RegionsRepository(UzWorksDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Region>> GetAllRegionsAsync()
    {
        return await _context.Regions.ToArrayAsync();
    }
}
