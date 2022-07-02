using BaseNet.Core.Entities.SystemLogs;
using BaseNet.Core.Models.SystemLogs;
using BaseNet.Core.Repositories.Commons;
using BaseNet.Core.Repositories.SystemLogs;

namespace BaseNet.Core.Services.SystemLogs
{
    public abstract class SystemLogService<T> where T : ISystemLogEntity
    {
        private readonly ISystemLogRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly LogMapper _mapper;

        public SystemLogService(
            ISystemLogRepository<T> repository,
            IUnitOfWork unitOfWork,
            LogMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> PutAsync(T entity) 
        {
            await _repository.AddAsync(entity);
            return await _unitOfWork.SaveEntitiesAsync();
        }

        public async Task<PaginatedResponse<T>> GetPagedAsync(DatePagedParameter parameter)
        {
            var dataCount = await _repository.GetCountAsync(parameter);
            var pageCount = (int)Math.Ceiling(dataCount / (double)parameter.Limit);
            var records = await _repository.GetLogsAsync(parameter);
            var response = _mapper.MapLogs(records, parameter.Offset, parameter.Limit, pageCount);
            return response;
        }

        public async Task<IEnumerable<T>> GetRangeAsync(DatePagedParameter parameter)
        {
            var records = await _repository.GetLogsAsync(parameter);
            return records;
        }
    }
}
