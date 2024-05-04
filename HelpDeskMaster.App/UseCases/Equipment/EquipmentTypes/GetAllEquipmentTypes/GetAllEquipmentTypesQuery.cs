using HelpDeskMaster.Domain.Entities.EquipmentTypes;
using MediatR;

namespace HelpDeskMaster.App.UseCases.Equipment.EquipmentTypes.GetAllEquipmentTypes
{
    public record GetAllEquipmentTypesQuery : IRequest<List<EquipmentType>>
    {
    }
}
