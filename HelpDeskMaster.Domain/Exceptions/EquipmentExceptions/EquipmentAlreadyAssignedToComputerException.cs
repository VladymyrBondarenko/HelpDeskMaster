namespace HelpDeskMaster.Domain.Exceptions.EquipmentExceptions
{
    public class EquipmentAlreadyAssignedToComputerException : DomainException
    {
        public EquipmentAlreadyAssignedToComputerException(Guid equipmentId)
            : base(DomainErrorCode.InternalServerError, $"Equipment with id {equipmentId} was already assigned to a computer")
        {
        }
    }
}
