
namespace HelpDeskMaster.Domain.Entities.Equipments
{
    public interface IComputerEquipmentService
    {
        Task AssignEquipmentToComputerAsync(Guid computerId, Guid equipmentId, DateTimeOffset assignDate, CancellationToken cancellationToken);
        Task UnassignEquipmentFromComputerAsync(Guid computerId, Guid equipmentId, DateTimeOffset unassignDate, CancellationToken cancellationToken);
    }
}