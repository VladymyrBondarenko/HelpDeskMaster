using Ardalis.GuardClauses;
using HelpDeskMaster.Domain.Abstractions;
using HelpDeskMaster.Domain.Entities.Equipments;

namespace HelpDeskMaster.Domain.Entities.Users
{
    public class UserEquipment : Entity
    {
        public UserEquipment(Guid id,
            DateTimeOffset createdAt,
            Guid userId,
            Guid equipmentId,
            DateTimeOffset assignedDate) : base(id, createdAt)
        {
            UserId = Guard.Against.Default(userId);
            EquipmentId = Guard.Against.Default(equipmentId);
            AssignedDate = Guard.Against.Default(assignedDate);
        }

        public Guid UserId { get; private set; }

        public Guid EquipmentId { get; private set; }

        public Equipment? Equipment { get; }

        public DateTimeOffset AssignedDate { get; private set; }

        public DateTimeOffset? UnassignedDate { get; private set; }

        public void UnassignEquipment(DateTimeOffset unassignDate)
        {
            UnassignedDate = Guard.Against.Default(unassignDate);
            UpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}