namespace HelpDeskMaster.WebApi.Contracts.Equipment.Requests
{
    public record AssignEquipmentToComputerRequest(
        Guid ComputerId,
        Guid EquipmentId,
        DateTimeOffset AssignDate)
    {
    }
}
