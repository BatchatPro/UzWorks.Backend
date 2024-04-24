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
        var region = new Region(regionDto.Name) ??
            throw new UzWorksException("Region Dto can't be null.");

        await _regionsRepository.CreateAsync(region);
        await _regionsRepository.SaveChanges();
        
        return _mappingService.Map<RegionVM, Region>(region);
    }

    public async Task<IEnumerable<RegionVM>> GetAllAsync()
    {
        var regions = await _regionsRepository.GetAllAsync();

        return _mappingService.Map<IEnumerable<RegionVM>, IEnumerable<Region>>(regions);
    }

    public async Task<RegionVM> GetById(Guid id)
    {
        var region = await _regionsRepository.GetById(id) ?? 
            throw new UzWorksException($"Could not find region with Id ; {id}");
        
        return _mappingService.Map<RegionVM, Region>(region);
    }

    public async Task<RegionVM> GetByDistrictId(Guid id)
    {
        var region = await _regionsRepository.GetByDistrictId(id) ?? 
            throw new UzWorksException($"Could not find region with District Id ; {id}");

        return _mappingService.Map<RegionVM, Region>(region);
    }

    public async Task<bool> IsExists(string regionName)
    {
        return await _regionsRepository.Exists(regionName);
    }

    public async Task<RegionVM> Update(RegionEM regionEM)
    {
        var region = await _regionsRepository.GetById(regionEM.Id) ??
            throw new UzWorksException($"Could not find region with Id ; {regionEM.Id}");

        _mappingService.Map(regionEM, region);
        _regionsRepository.UpdateAsync(region);
        await _regionsRepository.SaveChanges();

        return _mappingService.Map<RegionVM, Region>(region);
    }

    public async Task<bool> Delete(Guid id)
    {
        var region = await _regionsRepository.GetById(id) ?? 
            throw new UzWorksException($"Could not find region with Id ; {id}");

        _regionsRepository.Delete(region);
        await _regionsRepository.SaveChanges();

        return true;
    }
}
