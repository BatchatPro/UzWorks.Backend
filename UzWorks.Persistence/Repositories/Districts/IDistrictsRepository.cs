using UzWorks.Core.Entities.Location;

namespace UzWorks.Persistence.Repositories.Districts;

public interface IDistrictsRepository : IGenericRepository<District>
{
    Task<bool> IsExist(string districtName);

    Task<bool> IsExist(Guid districtId);

    Task<IEnumerable<District>> GetAllAsync();

    Task<IEnumerable<District>> GetByRegionIdAsync(Guid regionId);
}
