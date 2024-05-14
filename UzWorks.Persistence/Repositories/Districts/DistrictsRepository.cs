using Microsoft.EntityFrameworkCore;
using UzWorks.Core.Entities.Location;
using UzWorks.Persistence.Data;

namespace UzWorks.Persistence.Repositories.Districts;

public class DistrictsRepository : GenericRepository<District>, IDistrictsRepository
{
    public DistrictsRepository(UzWorksDbContext context) : base(context)
    {
    }

    public async Task<bool> IsExist(string? districtName)
    {
        return await _context.Districts.AnyAsync(d => d.Name == districtName);
    }

    public async Task<bool> IsExist(Guid districtId)
    {
        return await _context.Districts.AnyAsync(d => d.Id == districtId);
    }

    public async Task<IEnumerable<District>> GetAllAsync()
    {
        return await _context.Districts.OrderBy(x => x.Name).ToArrayAsync();
    }

    public async Task<IEnumerable<District>> GetByRegionIdAsync(Guid regionId)
    {
        return await _dbSet.Where(x => x.RegionId.Equals(regionId)).OrderBy(x => x.Name).ToArrayAsync();
    }
}
