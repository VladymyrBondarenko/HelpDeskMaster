namespace HelpDeskMaster.Domain.Entities.WorkDirections
{
    public interface IWorkDirectionRepository
    {
        Task InsertAsync(WorkDirection workDirection, CancellationToken cancellationToken);

        void Delete(Guid workDirectionId);

        Task<bool> IsWorkDirectionUsed(Guid workDirectionId, CancellationToken cancellationToken);
    }
}
