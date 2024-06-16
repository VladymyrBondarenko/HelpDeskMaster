namespace HelpDeskMaster.Domain.Entities.WorkRequests
{
    public interface IWorkRequestRepository
    {
        Task InsertAsync(WorkRequest workRequest, CancellationToken cancellationToken);
    }
}