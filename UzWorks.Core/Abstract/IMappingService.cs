namespace UzWorks.Core.Abstract;

public interface IMappingService
{
    T Map<T, TSourse>(TSourse sourse);

    T Map<T, TSource>(TSource source, T destination);
}
