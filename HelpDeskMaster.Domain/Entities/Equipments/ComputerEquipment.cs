using Ardalis.GuardClauses;
using HelpDeskMaster.Domain.Abstractions;

namespace HelpDeskMaster.Domain.Entities.Equipments
{
    public class ComputerEquipment : AggregateRoot
    {
        private ComputerEquipment() { }

        public ComputerEquipment(Guid id,
            DateTimeOffset createdAt, 
            Guid computerId,
            Guid equipmentId,
            DateTimeOffset assignDate) : base(id, createdAt)
        {
            ComputerId = Guard.Against.Default(computerId);
            EquipmentId = Guard.Against.Default(equipmentId);
            AssignedDate = Guard.Against.Default(assignDate);
        }

        public Guid ComputerId { get; private set; }

        public Equipment? Equipment { get; }

        public Guid EquipmentId { get; private set; }

        public DateTimeOffset AssignedDate { get; private set; }

        public DateTimeOffset? UnassignedDate { get; private set; }

        internal void UnassignEquipmentFromComputer(DateTimeOffset unassignDate)
        {
            UnassignedDate = Guard.Against.Default(unassignDate);
            UpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}