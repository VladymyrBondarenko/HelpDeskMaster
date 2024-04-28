using MediatR;

namespace HelpDeskMaster.App.UseCases.Equipment.Equipments.AssignEquipmentToComputer
{
    public record AssignEquipmentToComputerCommand(
        Guid ComputerId, 
        Guid EquipmentId, 
        DateTimeOffset AssignDate) : IRequest
    {
    }
}
