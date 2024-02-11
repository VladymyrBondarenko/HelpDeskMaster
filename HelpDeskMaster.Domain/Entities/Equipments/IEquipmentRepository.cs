namespace HelpDeskMaster.Domain.Entities.Equipments
{
    public interface IEquipmentRepository
    {
        void Insert(Equipment equipment);

        Task<bool> UpdateAsync(Equipment equipment, CancellationToken cancellationToken);

        Task<bool> DeleteAsync(Guid equipmentId, CancellationToken cancellationToken);
    }
}
