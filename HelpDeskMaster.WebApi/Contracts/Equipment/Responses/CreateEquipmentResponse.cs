namespace HelpDeskMaster.WebApi.Contracts.Equipment.Responses
{
    public record CreateEquipmentResponse(
        Guid Id,
        Guid EquipmentTypeId,
        string? Model,
        DateTimeOffset CommissioningDate,
        string? FactoryNumber,
        decimal Price,
        DateTimeOffset CreatedAt)
    {
    }
}
