namespace HelpDeskMaster.Domain.Entities.EquipmentTypes
{
    public interface IEquipmentTypeRepository
    {
        Task InsertAsync(EquipmentType equipmentType, CancellationToken cancellationToken);

        void Update(EquipmentType equipmentType);

        Task DeleteAsync(Guid equipmentTypeId, CancellationToken cancellationToken);
    }
}