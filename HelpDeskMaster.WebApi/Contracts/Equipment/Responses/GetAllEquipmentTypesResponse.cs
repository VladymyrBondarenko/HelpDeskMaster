namespace HelpDeskMaster.WebApi.Contracts.Equipment.Responses
{
    public record GetAllEquipmentTypesResponse(
        IReadOnlyList<EquipmentTypeModel> EquipmentTypes)
    {
    }
}
