using HelpDeskMaster.Domain.Entities.EquipmentTypes;

namespace HelpDeskMaster.WebApi.Contracts.Equipment.Responses
{
    public record GetComputerResponse(
        Guid Id,
        Guid EquipmentTypeId,
        EquipmentTypeModel? EquipmentType,
        string? Model,
        DateTimeOffset CommissioningDate,
        string? FactoryNumber,
        decimal Price,
        string Code,
        string NameInNet,
        int WarrantyMonths,
        DateTimeOffset InvoiceDate,
        DateTimeOffset WarrantyCardDate,
        DateTimeOffset CreatedAt,
        DateTimeOffset? UpdatedAt)
    {
    }
}
