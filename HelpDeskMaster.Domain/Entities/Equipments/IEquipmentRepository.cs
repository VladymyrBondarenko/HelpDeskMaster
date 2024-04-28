namespace HelpDeskMaster.Domain.Entities.Equipments
{
    public interface IEquipmentRepository
    {
        Task InsertAsync(Equipment equipment, CancellationToken cancellationToken);

        void Update(Equipment equipment);

        Task DeleteAsync(Guid equipmentId, CancellationToken cancellationToken);
        Task<Equipment?> GetEquipmentByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
