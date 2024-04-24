using UzWorks.BL.Services.Locations.Districts;
using UzWorks.BL.Services.Locations.Regions;
using UzWorks.Core.DataTransferObjects.Location.Districts;
using UzWorks.Core.DataTransferObjects.Location.Regions;
using UzWorks.Core.Enums.Location.Districts;
using UzWorks.Core.Enums.Location.Region;
using UzWorks.Infrastructure.Helpers;

namespace UzWorks.API.Middleware;

public class LocationInitializer1
{
    private readonly RequestDelegate _next;
    private readonly IRegionsService _regionsService;
    private readonly IDistrictService _districtService;

    public LocationInitializer1(RequestDelegate next, IRegionsService regionsService, IDistrictService districtService)
    {
        _next = next;
        _regionsService = regionsService;
        _districtService = districtService;
    }

    public async Task InvokeAsync(HttpContext context) 
    {
        await InitializeLocationsAsync();
        await _next(context);
    }

    private async Task InitializeLocationsAsync()
    {
        //For generate all regions data 
        foreach (RegionsEnum regionEnum in Enum.GetValues(typeof(RegionsEnum)))
        {
            var regionName = EnumHelper.GetDescription(regionEnum);

            if (!await _regionsService.IsExists(regionName))
                await _regionsService.Create(new RegionDto { Name = regionName});
        }
        
        var regions = await _regionsService.GetAllAsync();

        //For generate Andijan districts data
        foreach (DistrictsEnumAndijan districtsEnum in Enum.GetValues(typeof(DistrictsEnumAndijan)))
        {
            var districtName = EnumHelper.GetDescription(districtsEnum);

            var region = regions.FirstOrDefault(r => r.Name == EnumHelper.GetDescription(RegionsEnum.Andijan));

            if (!await _districtService.IsExist(districtName) && region != null)
                await _districtService.Create(new DistrictDto { Name = districtName, RegionId = region.Id });
        }

        //For generate Bukhara districts data
        foreach (DistrictsEnumBukhara districtsEnum in Enum.GetValues(typeof(DistrictsEnumBukhara)))
        {
            var districtName = EnumHelper.GetDescription(districtsEnum);

            var region = regions.FirstOrDefault(r => r.Name == EnumHelper.GetDescription(RegionsEnum.Bukhara));

            if (!await _districtService.IsExist(districtName) && region != null)
                await _districtService.Create(new DistrictDto { Name = districtName, RegionId = region.Id });
        }

        //For generate Fergana districts data
        foreach (DistrictsEnumFergana districtsEnum in Enum.GetValues(typeof(DistrictsEnumFergana)))
        {
            var districtName = EnumHelper.GetDescription(districtsEnum);

            var region = regions.FirstOrDefault(r => r.Name == EnumHelper.GetDescription(RegionsEnum.Fergana));

            if (!await _districtService.IsExist(districtName) && region != null)
                await _districtService.Create(new DistrictDto { Name = districtName, RegionId = region.Id });
        }

        //For generate Jizzakh districts data
        foreach (DistrictsEnumJizzakh districtsEnum in Enum.GetValues(typeof(DistrictsEnumJizzakh)))
        {
            var districtName = EnumHelper.GetDescription(districtsEnum);

            var region = regions.FirstOrDefault(r => r.Name == EnumHelper.GetDescription(RegionsEnum.Jizzakh));

            if (!await _districtService.IsExist(districtName) && region != null)
                await _districtService.Create(new DistrictDto { Name = districtName, RegionId = region.Id });
        }

        //For generate Karakalpakstan districts data
        foreach (DistrictsEnumKarakalpakstan districtsEnum in Enum.GetValues(typeof(DistrictsEnumKarakalpakstan)))
        {
            var districtName = EnumHelper.GetDescription(districtsEnum);

            var region = regions.FirstOrDefault(r => r.Name == EnumHelper.GetDescription(RegionsEnum.Karakalpakstan));

            if (!await _districtService.IsExist(districtName) && region != null)
                await _districtService.Create(new DistrictDto { Name = districtName, RegionId = region.Id }); 
        }

        //For generate Khorezm districts data
        foreach (DistrictsEnumKhorezm districtsEnum in Enum.GetValues(typeof(DistrictsEnumKhorezm)))
        {
            var districtName = EnumHelper.GetDescription(districtsEnum);

            var region = regions.FirstOrDefault(r => r.Name == EnumHelper.GetDescription(RegionsEnum.Xorazm));

            if (!await _districtService.IsExist(districtName) && region != null)
                await _districtService.Create(new DistrictDto { Name = districtName,RegionId = region.Id });
        }

        //For generate Namangan districts data
        foreach (DistrictsEnumNamangan districtsEnum in Enum.GetValues(typeof(DistrictsEnumNamangan)))
        {
            var districtName = EnumHelper.GetDescription(districtsEnum);

            var region = regions.FirstOrDefault(r => r.Name == EnumHelper.GetDescription(RegionsEnum.Namangan));

            if (!await _districtService.IsExist(districtName) && region != null)
                await _districtService.Create(new DistrictDto { Name = districtName, RegionId = region.Id });
        }

        //For generate Navoiy districts data
        foreach (DistrictsEnumNavoiy districtsEnum in Enum.GetValues(typeof(DistrictsEnumNavoiy)))
        {
            var districtName = EnumHelper.GetDescription(districtsEnum);

            var region = regions.FirstOrDefault(r => r.Name == EnumHelper.GetDescription(RegionsEnum.Navoiy));

            if (!await _districtService.IsExist(districtName) && region != null)
                await _districtService.Create(new DistrictDto { Name = districtName, RegionId = region.Id });
        }

        //For generate Qashqadaryo districts data
        foreach (DistrictsEnumQashqadaryo districtsEnum in Enum.GetValues(typeof(DistrictsEnumQashqadaryo)))
        {
            var districtName = EnumHelper.GetDescription(districtsEnum);

            var region = regions.FirstOrDefault(r => r.Name == EnumHelper.GetDescription(RegionsEnum.Qashqadaryo));

            if (!await _districtService.IsExist(districtName) && region != null)
                await _districtService.Create(new DistrictDto { Name = districtName, RegionId = region.Id });
        }

        //For generate Samarkand districts data
        foreach (DistrictsEnumSamarqand districtsEnum in Enum.GetValues(typeof(DistrictsEnumSamarqand)))
        {
            var districtName = EnumHelper.GetDescription(districtsEnum);

            var region = regions.FirstOrDefault(r => r.Name == EnumHelper.GetDescription(RegionsEnum.Samarqand));

            if (!await _districtService.IsExist(districtName) && region != null)
                await _districtService.Create(new DistrictDto { Name = districtName, RegionId = region.Id });
        }

        //For generate Sirdaryo districts data
        foreach (DistrictsEnumSirdaryo districtsEnum in Enum.GetValues(typeof(DistrictsEnumSirdaryo)))
        {
            var districtName = EnumHelper.GetDescription(districtsEnum);

            var region = regions.FirstOrDefault(r => r.Name == EnumHelper.GetDescription(RegionsEnum.Sirdaryo));

            if (!await _districtService.IsExist(districtName) && region != null)
                await _districtService.Create(new DistrictDto { Name = districtName, RegionId = region.Id });
        }

        //For generate Surkhandarya districts data
        foreach (DistrictsEnumSurkhandarya districtsEnum in Enum.GetValues(typeof(DistrictsEnumSurkhandarya)))
        {
            var districtName = EnumHelper.GetDescription(districtsEnum);

            var region = regions.FirstOrDefault(r => r.Name == EnumHelper.GetDescription(RegionsEnum.Surxondaryo));

            if (!await _districtService.IsExist(districtName) && region != null)
                await _districtService.Create(new DistrictDto { Name = districtName, RegionId = region.Id });
        }

        //For generate Tashkent districts data
        foreach (DistrictsEnumTashkent districtsEnum in Enum.GetValues(typeof(DistrictsEnumTashkent)))
        {
            var districtName = EnumHelper.GetDescription(districtsEnum);

            var region = regions.FirstOrDefault(r => r.Name == EnumHelper.GetDescription(RegionsEnum.Tashkent));

            if (!await _districtService.IsExist(districtName) && region != null)
                await _districtService.Create(new DistrictDto { Name = districtName, RegionId = region.Id });
        }

        //For generate Tashkent city districts data
        foreach (DistrictsEnumTashkentCity districtsEnum in Enum.GetValues(typeof(DistrictsEnumTashkentCity)))
        {
            var districtName = EnumHelper.GetDescription(districtsEnum);

            var region = regions.FirstOrDefault(r => r.Name == EnumHelper.GetDescription(RegionsEnum.TashkentCity));

            if (!await _districtService.IsExist(districtName) && region != null)
                await _districtService.Create(new DistrictDto { Name = districtName, RegionId = region.Id }); 
        }

    }
}
