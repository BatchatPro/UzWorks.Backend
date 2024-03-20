using UzWorks.Core.Abstract;
using UzWorks.Core.DataTransferObjects.Location.Districts;
using UzWorks.Core.Entities.Location;
using UzWorks.Core.Exceptions;
using UzWorks.Persistence.Repositories.Districts;

namespace UzWorks.BL.Services.Locations.Districts;

public class DistrictService : IDistrictService
{
    private readonly IDistrictsRepository _districtsRepository;
    private readonly IMappingService _mappingService;
    public DistrictService(IDistrictsRepository districtsRepository, IMappingService mappingService)
    {
        _districtsRepository = districtsRepository;
        _mappingService = mappingService;
    }

    public async Task<DistrictVM> Create(DistrictDto districtDto)
    {
        if (districtDto == null)
            throw new UzWorksException($"District Dto can not be null.");

        var district = new District(districtDto.Name, districtDto.RegionId);
        
        await _districtsRepository.CreateAsync(district);
        await _districtsRepository.SaveChanges();

        return _mappingService.Map<DistrictVM,District>(district);
    }

    public async Task<IEnumerable<DistrictVM>> GetAllAsync()
    {
        var districts = await _districtsRepository.GetAllAsync();
        
        if (districts is null)
            throw new UzWorksException($"Could not find Districts");

        return _mappingService.Map<IEnumerable<DistrictVM>, IEnumerable<District>>(districts);
    }

    public async Task<DistrictVM> GetById(Guid id)
    {
        var district = await _districtsRepository.GetById(id) ??
            throw new UzWorksException($"Could not find District with Id: {id}");

        return _mappingService.Map<DistrictVM,District>(district);
    }

    public async Task<IEnumerable<DistrictVM>> GetByRegionId(Guid regionId)
    {
        var districts = await _districtsRepository.GetByRegionIdAsync(regionId) ??
            throw new UzWorksException($"Could not find District with this region id: {regionId}");

        return _mappingService.Map<IEnumerable<DistrictVM>, IEnumerable<District>>(districts);
    }

    public Task<bool> IsExist(string districtName)
    {
        return _districtsRepository.IsExist(districtName);
    }

    public Task<bool> IsExist(Guid districtId)
    {
        return _districtsRepository.IsExist(districtId);
    }

    public async Task<DistrictVM> Update(DistrictEM districtEM)
    {
        var district = await _districtsRepository.GetById(districtEM.Id)??
            throw new UzWorksException($"Could not find District with Id: {districtEM.Id}");

        _mappingService.Map(districtEM, district);

        _districtsRepository.UpdateAsync(district);
        await _districtsRepository.SaveChanges();

        return _mappingService.Map<DistrictVM,District>(district);
    }

    public async Task<bool> Delete(Guid id)
    {
        var district = await _districtsRepository.GetById(id) ??
            throw new UzWorksException($"Could not find District with id: {id}");

        _districtsRepository.Delete(district);
        await _districtsRepository.SaveChanges();

        return true;
    }
}
