using BaseNet.Core.Repositories.SystemLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseNet.Core.Services.SystemLogs
{
    public class BaseLogReaderService<T>
    {
        ISystemLogRepository<T> _repository;
        LogMapper _mapper;

        public BaseLogReaderService(
            ISystemLogRepository<T> repository,
            LogMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginatedResponse<T>> GetPagedAsync(SystemLogPagedParameter parameter)
        {
            var dataCount = await _repository.GetCountAsync(parameter);
            var pageCount = (int)Math.Ceiling(dataCount / (double)parameter.Limit);
            var records = await _repository.GetLogsAsync(parameter);
            var response = _mapper.MapLogs(records, parameter.Offset, parameter.Limit, pageCount);
            return response;
        }

        public async Task<IEnumerable<T>> GetRangeAsync(SystemLogRangedParameter parameter)
        {
            var records = await _repository.GetLogsAsync(parameter);
            return records;
        }
    }
}
