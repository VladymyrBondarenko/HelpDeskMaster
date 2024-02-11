namespace HelpDeskMaster.Domain.Entities.EquipmentTypes
{
    public interface IEquipmentTypeRepository
    {
        void Insert(EquipmentType equipmentType);

        Task<bool> UpdateAsync(EquipmentType equipmentType, CancellationToken cancellationToken);

        Task<bool> DeleteAsync(Guid equipmentTypeId, CancellationToken cancellationToken);
    }
}