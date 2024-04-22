using AutoMapper;
using UzWorks.Core.DataTransferObjects.Contacts;
using UzWorks.Core.DataTransferObjects.Experiences;
using UzWorks.Core.DataTransferObjects.FAQs;
using UzWorks.Core.DataTransferObjects.FeedBacks;
using UzWorks.Core.DataTransferObjects.JobCategories;
using UzWorks.Core.DataTransferObjects.Jobs;
using UzWorks.Core.DataTransferObjects.Location.Districts;
using UzWorks.Core.DataTransferObjects.Location.Regions;
using UzWorks.Core.DataTransferObjects.Users;
using UzWorks.Core.DataTransferObjects.Workers;
using UzWorks.Core.Entities.Contacts;
using UzWorks.Core.Entities.Experiences;
using UzWorks.Core.Entities.FAQs;
using UzWorks.Core.Entities.Feedbacks;
using UzWorks.Core.Entities.JobAndWork;
using UzWorks.Core.Entities.Location;
using UzWorks.Identity.Models;

namespace UzWorks.Infrastructure.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile() 
    {
        CreateMap<JobCategoryDto, JobCategory>();
        CreateMap<JobCategoryEM, JobCategory>();
        CreateMap<JobCategory, JobCategoryVM>();

        CreateMap<DistrictDto, District>();
        CreateMap<DistrictEM, District>();
        CreateMap<District, DistrictVM>();

        CreateMap<RegionDto, Region>();
        CreateMap<RegionEM, Region>();
        CreateMap<Region, RegionVM>();

        CreateMap<WorkerDto, Worker>();
        CreateMap<WorkerEM, Worker>();
        CreateMap<Worker, WorkerVM>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.JobCategory.Title))
            .ForMember(dest => dest.DistrictName, opt => opt.MapFrom(src => src.District.Name))
            .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => src.District.Region.Name));

        CreateMap<JobDto, Job>();
        CreateMap<JobEM, Job>();
        CreateMap<Job, JobVM>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.JobCategory.Title))
            .ForMember(dest => dest.DistrictName, opt => opt.MapFrom(src => src.District.Name))
            .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => src.District.Region.Name));

        CreateMap<UserEM, User>();
        CreateMap<UserDto, User>();
        CreateMap<User, UserVM>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()));

        CreateMap<ExperienceDto, Experience>();
        CreateMap<Experience, ExperienceVM>();
        CreateMap<ExperienceEM, Experience>();

        CreateMap<ContactDto, Contact>();
        CreateMap<Contact, ContactVM>();
        CreateMap<ContactEM, Contact>();

        CreateMap<FAQDto, FAQ>();
        CreateMap<FAQ, FAQVM>();
        CreateMap<FAQEM, FAQ>();

        CreateMap<FeedBackDto, FeedBack>();
        CreateMap<FeedBack, FeedBackVM>();
        CreateMap<FeedBackEM, FeedBack>();
    }
}
