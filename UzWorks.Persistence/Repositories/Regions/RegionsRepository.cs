using Microsoft.EntityFrameworkCore;
using UzWorks.Core.Entities.Location;
using UzWorks.Persistence.Data;

namespace UzWorks.Persistence.Repositories.Regions;

public class RegionsRepository : GenericRepository<Region>, IRegionsRepository
{
    public RegionsRepository(UzWorksDbContext context) : base(context)
    {
    }

    public async Task<bool> Exists(string regionName)
    {
        return await _context.Regions.AnyAsync(r => r.Name == regionName);
    }

    public async Task<IEnumerable<Region>> GetAllAsync()
    {
        return await _context.Regions.OrderBy(x => x.Name).ToArrayAsync();
    }

    public async Task<Region> GetByDistrictId(Guid id)
    {
        return await _context.Regions.FirstOrDefaultAsync(r => r.Districts.Any(d => d.Id == id));
    }
}
