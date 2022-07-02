using BaseNet.Core.Entities.Configs;
using BaseNet.Core.Repositories.Configs;
using Microsoft.EntityFrameworkCore;

namespace BaseNet.Infrastructure.Repositories.Configs
{
    public class ConfigurationRepository<TEntity> : IConfigRepository<TEntity>
        where TEntity : class, IConfigEntity
    {
        private readonly DbSet<TEntity> _entities;

        public ConfigurationRepository(DbSet<TEntity> entities)
        {
            _entities = entities;
        }

        public async Task<TEntity> GetAsync()
        {
            return await _entities.FirstAsync();
        }

        public Task<TEntity> SetAsync(TEntity config)
        {
            var entity = _entities.Update(config);
            return Task.FromResult(entity.Entity);
        }
    }
}
