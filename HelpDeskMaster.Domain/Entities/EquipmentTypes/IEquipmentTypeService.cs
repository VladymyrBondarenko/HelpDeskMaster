
namespace HelpDeskMaster.Domain.Entities.EquipmentTypes
{
    public interface IEquipmentTypeService
    {
        Task<EquipmentType> CreateEquipmentTypeAsync(string title, TypeOfEquipment typeOfEquipment, CancellationToken cancellationToken);
        Task DeleteEquipmentTypeAsync(Guid equipmentTypeId, CancellationToken cancellationToken);
        Task UpdateEquipmentTypeAsync(EquipmentType equipmentType, CancellationToken cancellationToken);
    }
}