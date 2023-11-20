using System.Runtime.CompilerServices;
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

        return _mappingService.Map<DistrictVM,District>(district);
    }

    public async Task Delete(Guid id)
    {
        var district = await _districtsRepository.GetById(id);
        
        if (district is null)
            throw new UzWorksException($"Could not find District with id: {id}");

        _districtsRepository.Delete(district);
        await _districtsRepository.SaveChanges();
    }

    public async Task<IEnumerable<DistrictVM>> GetAllAsync()
    {
        var districts = await _districtsRepository.GetAllDistrictsAsync();
        
        if (districts is null)
            throw new UzWorksException($"Could not find Districts");

        return _mappingService.Map<IEnumerable<DistrictVM>, IEnumerable<District>>(districts);
    }

    public async Task<DistrictVM> GetById(Guid id)
    {
        var district = await _districtsRepository.GetById(id);
        
        if (district is null)
            throw new UzWorksException($"Could not find District with Id: {id}");

        return _mappingService.Map<DistrictVM,District>(district);
    }

    public async Task<IEnumerable<DistrictVM>> GetDistrictByRegionId(Guid regionId)
    {
        var districts = await _districtsRepository.GetDistrictsByRegionIdAsync(regionId);
        
        if (districts is null)
            throw new UzWorksException($"Could not find District with this region id: {regionId}");

        return _mappingService.Map<IEnumerable<DistrictVM>, IEnumerable<District>>(districts);
    }

    public async Task<DistrictVM> Update(DistrictEM districtEM)
    {
        if (districtEM is null) 
            throw new UzWorksException("District edit model can not be null.");

        var district = _mappingService.Map<District, DistrictEM>(districtEM);
        await _districtsRepository.CreateAsync(district);
        await _districtsRepository.SaveChanges();
        return _mappingService.Map<DistrictVM,District>(district);
    }
}
