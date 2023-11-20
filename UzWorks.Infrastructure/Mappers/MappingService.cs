using AutoMapper;
using UzWorks.Core.Abstract;

namespace UzWorks.Infrastructure.Mappers;

public class MappingService : IMappingService
{
    private readonly IMapper mapper;

    public MappingService(IMapper mapper)
    {
        this.mapper = mapper;
    }

    public T Map<T, TSource>(TSource source)
    {
        return mapper.Map<T>(source);
    }

    public T Map<T, TSource>(TSource source, T destination)
    {
        mapper.Map(source, destination);
        return destination;
    }
}
