using HelpDeskMaster.Domain.Entities.EquipmentTypes;

namespace HelpDeskMaster.WebApi.Contracts.Equipment.Responses
{
    public record CreateEquipmentTypeResponse(
        Guid Id,
        string Title,
        TypeOfEquipment TypeOfEquipment,
        DateTimeOffset CreatedAt)
    {
    }
}
