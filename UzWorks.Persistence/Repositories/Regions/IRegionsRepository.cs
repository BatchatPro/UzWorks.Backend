using UzWorks.Core.DataTransferObjects.Location.Regions;
using UzWorks.Core.Entities.Location;

namespace UzWorks.Persistence.Repositories.Regions;

public interface IRegionsRepository : IGenericRepository<Region>
{
    Task<bool> Exists(string regionName);
    Task<IEnumerable<Region>> GetAllAsync();
    Task<Region> GetByDistrictId(Guid id);
}
