using UzWorks.Core.DataTransferObjects.Location.Regions;

namespace UzWorks.BL.Services.Locations.Regions;

public interface IRegionsService
{
    Task<RegionVM> Create(RegionDto regionDto);
    Task<IEnumerable<RegionVM>> GetAllAsync();
    Task<RegionVM> GetById(Guid id);
    Task<RegionVM> GetByDistrictId(Guid id);
    Task<bool> IsExists(string regionName);
    Task<RegionVM> Update(RegionEM regionEM);
    Task<bool> Delete(Guid Id);
}
