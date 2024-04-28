using MediatR;

namespace HelpDeskMaster.App.UseCases.Equipment.Equipments.CreateEquipment
{
    public record CreateEquipmentCommand(
        Guid EquipmentTypeId,
        string? Model,
        DateTimeOffset CommissioningDate,
        string? FactoryNumber,
        decimal Price) : IRequest<Domain.Entities.Equipments.Equipment>
    {
    }
}
