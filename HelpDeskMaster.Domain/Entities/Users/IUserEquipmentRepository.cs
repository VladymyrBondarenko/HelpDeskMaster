namespace HelpDeskMaster.Domain.Entities.Users
{
    public interface IUserEquipmentRepository
    {
        Task<bool> IsEquipmentAssignedAsync(Guid equipmentId, DateTimeOffset assignDate, CancellationToken cancellationToken);

        Task<bool> IsEquipmentAssignedToUserAsync(Guid equipmentId, Guid userId, DateTimeOffset date, CancellationToken cancellationToken);

        Task InsertAsync(UserEquipment userEquipment, CancellationToken cancellationToken);
    }
}