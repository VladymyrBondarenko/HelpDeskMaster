
namespace HelpDeskMaster.Domain.Entities.Equipments
{
    public interface IEquipmentService
    {
        Task<Equipment> CreateComputerAsync(Guid equipmentTypeId, string? model, DateTimeOffset commissioningDate, string? factoryNumber, decimal price, string code, string nameInNet, int warrantyMonths, DateTimeOffset invoiceDate, DateTimeOffset warrantyCardDate, CancellationToken cancellationToken);
        Task<Equipment> CreateEquipmentAsync(Guid equipmentTypeId, string? model, DateTimeOffset commissioningDate, string? factoryNumber, decimal price, CancellationToken cancellationToken);
        Task DeleteEquipmentAsync(Guid equipmentId, CancellationToken cancellationToken);
        Task UpdateEquipmentAsync(Equipment equipment, CancellationToken cancellationToken);
    }
}