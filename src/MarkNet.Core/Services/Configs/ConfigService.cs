using MarkNet.Core.Models;
using MarkNet.Core.Repositories.Commons;
using MarkNet.Core.Repositories.Configs;
using MarkNet.Core.Services.Cashings;

namespace MarkNet.Core.Services.Configs
{
    public abstract class ConfigService<T, TEntity>
        where T : PropertyModel<T>, new()
        where TEntity : T, new()
    {
        private readonly CashManager<T> _cashManager;
        private readonly IConfigRepository<TEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ConfigService(
            CashManager<T> cashManager,
            IConfigRepository<TEntity> repository,
            IUnitOfWork unitOfWork)
        {
            _cashManager = cashManager;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task InitializeAsync()
        {
            var entity = await _repository.GetAsync();
            var model = new T();
            model.CopyValues(entity);
            _cashManager.Set(model);
        }

        public Task<T> GetAsync()
        {
            var values = _cashManager.Get();
            return Task.FromResult(values);
        }

        public async Task SetAsync(T values)
        {
            var entity = new TEntity();
            entity.CopyValues(values);

            await _repository.SetAsync(entity);
            await _unitOfWork.SaveChangeAsync();

            _cashManager.Set(values);
        }
    }
}
