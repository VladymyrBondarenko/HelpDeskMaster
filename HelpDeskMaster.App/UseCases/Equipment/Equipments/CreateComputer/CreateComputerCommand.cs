using MediatR;

namespace HelpDeskMaster.App.UseCases.Equipment.Equipments.CreateComputer
{
    public record CreateComputerCommand(
        Guid EquipmentTypeId,
        string? Model,
        DateTimeOffset CommissioningDate,
        string? FactoryNumber,
        decimal Price,
        string Code,
        string NameInNet,
        int WarrantyMonths,
        DateTimeOffset InvoiceDate,
        DateTimeOffset WarrantyCardDate) : IRequest<Domain.Entities.Equipments.Equipment>
    {
    }
}
