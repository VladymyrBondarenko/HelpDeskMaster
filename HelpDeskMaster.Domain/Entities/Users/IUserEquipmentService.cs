
namespace HelpDeskMaster.Domain.Entities.Users
{
    public interface IUserEquipmentService
    {
        Task AssignEquipmentToUserAsync(Guid userId, Guid equipmentId, DateTimeOffset assignDate, CancellationToken cancellationToken);
        Task UnassignEquipmentFromUserAsync(Guid userId, Guid equipmentId, DateTimeOffset unassignDate, CancellationToken cancellationToken);
    }
}