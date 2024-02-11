namespace HelpDeskMaster.Domain.Exceptions.EquipmentExceptions
{
    public class AssignEquipmentNotToComputerException : DomainException
    {
        public AssignEquipmentNotToComputerException(Guid equipmentId)
            : base(DomainErrorCode.BadRequest, $"Equipment with id {equipmentId} is not a computer type equipment")
        {
        }
    }
}