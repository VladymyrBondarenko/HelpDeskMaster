namespace HelpDeskMaster.Domain.Exceptions.EquipmentExceptions
{
    public class AssignToComputerNotEquipmentException : DomainException
    {
        public AssignToComputerNotEquipmentException(Guid equipmentId)
            : base(DomainErrorCode.BadRequest, $"Equipment with id {equipmentId} is not a equipment type")
        {
        }
    }
}