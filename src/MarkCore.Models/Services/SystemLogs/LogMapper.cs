using BaseNet.Core.Models.SystemLogs;

namespace BaseNet.Core.Services.SystemLogs
{
    public PaginatedResponse<T> MapLogs<T>(IEnumerable<T> logs, int offset, int limit, int total)
    {
        return new PaginatedResponse<T>()
        {
            Metadata = new PaginatedResponseMetadata()
            {
                Offset = offset,
                Limit = limit,
                Total = total,
            },
            Records = logs
        };
    }
}
