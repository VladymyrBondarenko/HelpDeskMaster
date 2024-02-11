namespace HelpDeskMaster.Domain.Exceptions.WorkRequestExceptions
{
    public class EquipmentIsNotAssignedToWorkRequestAuthorException : DomainException
    {
        public EquipmentIsNotAssignedToWorkRequestAuthorException(Guid equipmentId, Guid authorId) 
            : base(DomainErrorCode.InternalServerError, $"Equipment with id {equipmentId} is not assigned to user with id {authorId}")
        {
        }
    }
}
