namespace HelpDeskMaster.Domain.Exceptions.EquipmentExceptions
{
    public class ComputerEquipmentNotFoundToUnassignException : DomainException
    {
        public ComputerEquipmentNotFoundToUnassignException(Guid computerId, Guid equipmentId) 
            : base(DomainErrorCode.InternalServerError, $"Could not find equipment with id {equipmentId} for computer with id {computerId} for unassigning")
        {
        }
    }
}