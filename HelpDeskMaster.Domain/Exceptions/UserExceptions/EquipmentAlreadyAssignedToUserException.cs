namespace HelpDeskMaster.Domain.Exceptions.UserExceptions
{
    public class EquipmentAlreadyAssignedToUserException : DomainException
    {
        public EquipmentAlreadyAssignedToUserException(Guid equipmentId)
            : base(DomainErrorCode.BadRequest, $"Equipment with id {equipmentId} was already assigned to a user")
        {
        }
    }
}
