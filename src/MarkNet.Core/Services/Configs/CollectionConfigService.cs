using MarkNet.Core.Models;
using MarkNet.Core.Repositories.Commons;
using MarkNet.Core.Repositories.Configs;
using MarkNet.Core.Services.Cashings;

namespace MarkNet.Core.Services.Configs
{
    public abstract class CollectionConfigService<T, TEntity>
        where T : PropertyModel<T>, new()
        where TEntity : T, new()
    {
        private readonly CollectionCashManager<T> _cashManager;
        private readonly ICollectionConfigRepository<TEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CollectionConfigService(
            CollectionCashManager<T> cashManager,
            ICollectionConfigRepository<TEntity> repository,
            IUnitOfWork unitOfWork)
        {
            _cashManager = cashManager;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task InitializeAsync()
        {
            var values = (await _repository.GetAllAsync())
                .Select(row =>
                {
                    var model = new T();
                    model.CopyValues(row);
                    return model;
                });

            _cashManager.Set(values);
        }

        public Task<IEnumerable<T>> GetAsync()
        {
            var values = _cashManager.Get();
            return Task.FromResult(values);
        }

        public async Task SetAsync(IEnumerable<T> values)
        {
            var entities = values.Select(row =>
            {
                var entity = new TEntity();
                entity.CopyValues(row);
                return entity;
            });

            await _repository.AddRangeAsync(entities);
            await _unitOfWork.SaveChangeAsync();

            _cashManager.Set(values);
        }
    }
}
