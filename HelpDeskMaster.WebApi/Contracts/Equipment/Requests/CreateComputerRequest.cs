namespace HelpDeskMaster.WebApi.Contracts.Equipment.Requests
{
    public record CreateComputerRequest(
        Guid EquipmentTypeId,
        string? Model,
        DateTimeOffset CommissioningDate,
        string? FactoryNumber,
        decimal Price,
        string Code,
        string NameInNet,
        int WarrantyMonths,
        DateTimeOffset InvoiceDate,
        DateTimeOffset WarrantyCardDate)
    {
    }
}