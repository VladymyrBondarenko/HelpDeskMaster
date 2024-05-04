namespace HelpDeskMaster.WebApi.Contracts.User.Request
{
    public record AssignEquipmentToUserRequest(
        Guid UserId,
        Guid EquipmentId,
        DateTimeOffset AssignDate)
    {
    }
}