using AutoMapper;
using UzWorks.Core.DataTransferObjects.JobCategories;
using UzWorks.Core.DataTransferObjects.Jobs;
using UzWorks.Core.DataTransferObjects.Location.Districts;
using UzWorks.Core.DataTransferObjects.Location.Regions;
using UzWorks.Core.DataTransferObjects.Workers;
using UzWorks.Core.Entities.JobAndWork;
using UzWorks.Core.Entities.Location;

namespace UzWorks.Infrastructure.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile() 
    {
        CreateMap<JobCategory, JobCategoryVM>().ReverseMap();
        CreateMap<JobCategory, JobCategoryDto>().ReverseMap();
        CreateMap<JobCategory, JobCategoryEM>().ReverseMap();

        CreateMap<District, DistrictDto>().ReverseMap();
        CreateMap<District, DistrictVM>().ReverseMap();

        CreateMap<Region,RegionDto>().ReverseMap();
        CreateMap<Region,RegionVM>().ReverseMap();

        CreateMap<Worker,WorkerDto>().ReverseMap();
        CreateMap<Worker,WorkerVM>().ReverseMap();

        CreateMap<Job,JobDto>().ReverseMap();
        CreateMap<Job,JobVM>().ReverseMap();
    }
}
