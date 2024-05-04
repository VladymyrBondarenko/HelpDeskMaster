namespace HelpDeskMaster.WebApi.Contracts.Equipment.Requests
{
    public record CreateEquipmentRequest(
        Guid EquipmentTypeId, 
        string? Model, 
        DateTimeOffset CommissioningDate, 
        string? FactoryNumber, 
        decimal Price)
    {
    }
}
