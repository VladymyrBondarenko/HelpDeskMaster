namespace HelpDeskMaster.WebApi.Contracts.Equipment.Responses
{
    public class GetAllEquipmentTypesResponse
    {
        public required IReadOnlyList<EquipmentTypeModel> EquipmentTypes { get; set; }
    }
}
