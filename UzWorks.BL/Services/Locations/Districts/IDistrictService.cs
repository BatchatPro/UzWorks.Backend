using UzWorks.Core.DataTransferObjects.Location.Districts;

namespace UzWorks.BL.Services.Locations.Districts;

public interface IDistrictService
{
    Task<DistrictVM> Create(DistrictDto districtDto);
    Task<IEnumerable<DistrictVM>> GetAllAsync();
    Task<DistrictVM> GetById(Guid id);
    Task<IEnumerable<DistrictVM>> GetByRegionId(Guid regionId);
    Task<bool> IsExist(string districtName);
    Task<bool> IsExist(Guid districtId);
    Task<DistrictVM> Update(DistrictEM districtEM);
    Task<bool> Delete(Guid id);
}
