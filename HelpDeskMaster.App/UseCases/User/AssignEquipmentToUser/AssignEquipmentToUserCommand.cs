using MediatR;

namespace HelpDeskMaster.App.UseCases.User.AssignEquipmentToUser
{
    public record AssignEquipmentToUserCommand(
        Guid UserId, 
        Guid EquipmentId, 
        DateTimeOffset AssignDate) : IRequest
    {
    }
}
