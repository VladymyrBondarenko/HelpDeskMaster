using HelpDeskMaster.Domain.Entities.EquipmentTypes;
using MediatR;

namespace HelpDeskMaster.App.UseCases.Equipment.EquipmentTypes.CreateEquipmentType
{
    public record class CreateEquipmentTypeCommand(
        string Title,
        TypeOfEquipment TypeOfEquipment) : IRequest<EquipmentType>
    {
    }
}
