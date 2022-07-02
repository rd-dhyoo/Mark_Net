namespace BaseNet.Core.Repositories.Configs
{
    public interface ICollectionConfigRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> configs);
        Task<IEnumerable<T>> RemoveAllAsync(IEnumerable<T> entities);
    }
}
