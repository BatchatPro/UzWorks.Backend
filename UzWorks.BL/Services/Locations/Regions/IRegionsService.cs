using UzWorks.Core.DataTransferObjects.Location.Regions;

namespace UzWorks.BL.Services.Locations.Regions;

public interface IRegionsService
{
    Task<IEnumerable<RegionVM>> GetAllAsync();
    Task<RegionVM> GetById(Guid id);
    Task<RegionVM> Create(RegionDto regionDto);
    Task Delete(Guid Id);
    Task<RegionVM> Update(RegionEM regionEM);
}
