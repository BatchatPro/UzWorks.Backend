using UzWorks.Core.Entities.Location;

namespace UzWorks.Persistence.Repositories.Regions;

public interface IRegionsRepository : IGenericRepository<Region>
{
    Task<IEnumerable<Region>> GetAllRegionsAsync();
}
