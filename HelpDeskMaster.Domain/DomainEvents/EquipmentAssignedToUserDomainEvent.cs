using HelpDeskMaster.Domain.Abstractions;

namespace HelpDeskMaster.Domain.DomainEvents
{
    public sealed record EquipmentAssignedToUserDomainEvent(
        Guid UserId, 
        Guid EquipmentId, 
        DateTimeOffset AssignedDate) : IDomainEvent
    {
    }
}
