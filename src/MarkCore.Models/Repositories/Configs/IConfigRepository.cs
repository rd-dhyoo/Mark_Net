namespace BaseNet.Core.Repositories.Configs
{
    public interface IConfigRepository<T>
    {
        Task<T> GetAsync();
        Task<T> SetAsync(T config);
    }
}
