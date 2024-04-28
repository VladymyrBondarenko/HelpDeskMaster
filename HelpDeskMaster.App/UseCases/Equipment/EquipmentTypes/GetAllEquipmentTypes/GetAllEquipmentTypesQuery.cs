using HelpDeskMaster.Domain.Entities.EquipmentTypes;
using MediatR;

namespace HelpDeskMaster.App.UseCases.Equipment.EquipmentTypes.GetAllEquipmentTypes
{
    public class GetAllEquipmentTypesQuery : IRequest<List<EquipmentType>>
    {
    }
}
