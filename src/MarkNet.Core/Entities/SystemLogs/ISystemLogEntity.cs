namespace MarkNet.Core.Entities.SystemLogs
{
    public interface ISystemLogEntity
    {
        int Id { get; set; }
        DateTime Created { get; set; }
    }
}
