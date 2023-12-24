using Microsoft.EntityFrameworkCore;
using UzWorks.Core.Entities.Location;
using UzWorks.Persistence.Data;

namespace UzWorks.Persistence.Repositories.Districts;

public class DistrictsRepository : GenericRepository<District>, IDistrictsRepository
{
    public DistrictsRepository(UzWorksDbContext context) : base(context)
    {
    }

    public async Task<bool> Exists(string districtName)
    {
        return await _context.Districts.AnyAsync(d => d.Name == districtName);
    }

    public async Task<IEnumerable<District>> GetAllDistrictsAsync()
    {
        return await _context.Districts.ToArrayAsync();
    }

    public async Task<IEnumerable<District>> GetDistrictsByRegionIdAsync(Guid regionId)
    {
        return await _dbSet.Where(x => x.RegionId.Equals(regionId)).ToArrayAsync();
    }
}
