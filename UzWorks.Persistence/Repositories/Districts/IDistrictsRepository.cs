using UzWorks.Core.Entities.Location;

namespace UzWorks.Persistence.Repositories.Districts;

public interface IDistrictsRepository : IGenericRepository<District>
{
    Task<IEnumerable<District>> GetAllDistrictsAsync();

    Task<IEnumerable<District>> GetDistrictsByRegionIdAsync(Guid regionId);
}
