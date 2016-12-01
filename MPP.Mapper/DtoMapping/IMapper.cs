namespace DtoMapping
{
    public interface IMapper
    {
        TDestination Map<TSource, TDestination>(TSource source) where TSource : class where TDestination : class, new();
    }
}
