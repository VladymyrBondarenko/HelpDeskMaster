
namespace HelpDeskMaster.Domain.Entities.WorkDirections
{
    public interface IWorkDirectionService
    {
        Task<WorkDirection> CreateWorkDirectionAsync(string title, CancellationToken cancellationToken);
        Task DeleteWorkDirectionAsync(Guid workDirectionId, CancellationToken cancellationToken);
    }
}