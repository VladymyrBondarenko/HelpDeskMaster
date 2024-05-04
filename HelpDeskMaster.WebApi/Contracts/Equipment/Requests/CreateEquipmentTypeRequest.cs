using HelpDeskMaster.Domain.Entities.EquipmentTypes;

namespace HelpDeskMaster.WebApi.Contracts.Equipment.Requests
{
    public record CreateEquipmentTypeRequest(
        string Title, 
        TypeOfEquipment TypeOfEquipment)
    {
    }
}
