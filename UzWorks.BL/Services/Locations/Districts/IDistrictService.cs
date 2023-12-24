using UzWorks.Core.DataTransferObjects.Location.Districts;

namespace UzWorks.BL.Services.Locations.Districts;

public interface IDistrictService
{
    Task<IEnumerable<DistrictVM>> GetAllAsync();
    Task<IEnumerable<DistrictVM>> GetDistrictByRegionId(Guid regionId);
    Task<DistrictVM> GetById(Guid id);
    Task<DistrictVM> Create(DistrictDto districtDto);
    Task<DistrictVM> Update(DistrictEM districtEM);
    Task Delete(Guid id);
    Task<bool> Exists(string districtName);
}
