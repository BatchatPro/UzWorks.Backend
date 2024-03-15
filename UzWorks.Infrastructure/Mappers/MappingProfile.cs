using AutoMapper;
using UzWorks.Core.DataTransferObjects.Contacts;
using UzWorks.Core.DataTransferObjects.Experiences;
using UzWorks.Core.DataTransferObjects.FAQs;
using UzWorks.Core.DataTransferObjects.JobCategories;
using UzWorks.Core.DataTransferObjects.Jobs;
using UzWorks.Core.DataTransferObjects.Location.Districts;
using UzWorks.Core.DataTransferObjects.Location.Regions;
using UzWorks.Core.DataTransferObjects.Users;
using UzWorks.Core.DataTransferObjects.Workers;
using UzWorks.Core.Entities.Contacts;
using UzWorks.Core.Entities.Experiences;
using UzWorks.Core.Entities.FAQs;
using UzWorks.Core.Entities.JobAndWork;
using UzWorks.Core.Entities.Location;
using UzWorks.Identity.Models;

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
        CreateMap<District, DistrictEM>().ReverseMap();

        CreateMap<Region,RegionDto>().ReverseMap();
        CreateMap<Region,RegionVM>().ReverseMap();
        CreateMap<Region,RegionEM>().ReverseMap();

        CreateMap<Worker,WorkerDto>().ReverseMap();
        CreateMap<Worker,WorkerVM>().ReverseMap();
        CreateMap<Worker,WorkerEM>().ReverseMap();

        CreateMap<Job,JobDto>().ReverseMap();
        CreateMap<Job,JobVM>().ReverseMap();
        CreateMap<Job,JobEM>().ReverseMap();

        CreateMap<User,UserEM>().ReverseMap();
        CreateMap<User,UserDto>().ReverseMap();
        CreateMap<User,UserVM>().ReverseMap();

        CreateMap<Experience,ExperienceDto>().ReverseMap();
        CreateMap<Experience,ExperienceVM>().ReverseMap();
        CreateMap<Experience,ExperienceEM>().ReverseMap();

        CreateMap<Contact,ContactDto>().ReverseMap();
        CreateMap<Contact,ContactVM>().ReverseMap();
        CreateMap<Contact,ContactEM>().ReverseMap();

        CreateMap<FAQ,FAQDto>().ReverseMap();
        CreateMap<FAQ,FAQVM>().ReverseMap();
        CreateMap<FAQ,FAQEM>().ReverseMap();
    }
}
