using Ardalis.GuardClauses;
using HelpDeskMaster.Domain.Abstractions;

namespace HelpDeskMaster.Domain.Entities.WorkRequests
{
    public class WorkRequestEquipment : Entity
    {
        public WorkRequestEquipment(Guid id, DateTimeOffset createdAt, 
            Guid workRequestId, 
            Guid equipmentId) : base(id, createdAt)
        {
            WorkRequestId = Guard.Against.Default(workRequestId);
            EquipmentId = Guard.Against.Default(equipmentId);
        }

        public Guid WorkRequestId { get; private set; }

        public Guid EquipmentId { get; private set; }
    }
}