namespace HelpDeskMaster.WebApi.Contracts.Equipment.Responses
{
    public record CreateComputerResponse(
        Guid Id,
        Guid EquipmentTypeId,
        string? Model,
        DateTimeOffset CommissioningDate,
        string? FactoryNumber,
        decimal Price,
        string Code,
        string NameInNet,
        int WarrantyMonths,
        DateTimeOffset InvoiceDate,
        DateTimeOffset CreatedAt)
    {
    }
}