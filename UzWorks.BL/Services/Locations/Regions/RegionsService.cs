using System.Net.Http.Headers;
using UzWorks.Core.Abstract;
using UzWorks.Core.DataTransferObjects.Location.Regions;
using UzWorks.Core.Entities.Location;
using UzWorks.Core.Exceptions;
using UzWorks.Persistence.Repositories.Regions;

namespace UzWorks.BL.Services.Locations.Regions;

public class RegionsService : IRegionsService
{
    private readonly IRegionsRepository _regionsRepository;
    private readonly IMappingService _mappingService;

    public RegionsService(IRegionsRepository regionsRepository, IMappingService mappingService)
    {
        _regionsRepository = regionsRepository;
        _mappingService = mappingService;
    }

    public async Task<RegionVM> Create(RegionDto regionDto)
    {
        if (regionDto == null)
            throw new UzWorksException("Region Dto can't be null.");

        var region = new Region(regionDto.Name);

        await _regionsRepository.CreateAsync(region);
        await _regionsRepository.SaveChanges();
        
        var result = _mappingService.Map<RegionVM, Region>(region);

        return result;
    }

    public async Task Delete(Guid id)
    {
        var region = await _regionsRepository.GetById(id);
        if (region is null) return;
        _regionsRepository.Delete(region);
        await _regionsRepository.SaveChanges();
    }

    public async Task<bool> Exists(string regionName)
    {
        return await _regionsRepository.Exists(regionName);
    }

    public async Task<IEnumerable<RegionVM>> GetAllAsync()
    {
        var regions = await _regionsRepository.GetAllRegionsAsync();
        var result = _mappingService.Map<IEnumerable<RegionVM>, IEnumerable<Region>>(regions);
        return result;
    }

    public async Task<RegionVM> GetById(Guid id)
    {
        var region = await _regionsRepository.GetById(id);
        
        return region is null
            ?throw new UzWorksException($"Could not find region with Id ; {id}")
            : _mappingService.Map<RegionVM, Region>(region);
    }

    public async Task<RegionVM> GetByRegionByDistrictId(Guid id)
    {
        var region = await _regionsRepository.GetRegionByDistrictId(id);
        return _mappingService.Map<RegionVM, Region>(region);
    }

    public async Task<RegionVM> Update(RegionEM regionEM)
    {
        var region = await _regionsRepository.GetById(regionEM.Id) 
            ?? throw new UzWorksException($"Could not find region with Id ; {regionEM.Id}");

        _mappingService.Map(regionEM, region);
        _regionsRepository.UpdateAsync(region);
        await _regionsRepository.SaveChanges();

        return _mappingService.Map<RegionVM, Region>(region);
    }
}
