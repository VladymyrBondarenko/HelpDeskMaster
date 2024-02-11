namespace HelpDeskMaster.Domain.Exceptions.UserExceptions
{
    public class UserEquipmentNotFoundToUnassignException : DomainException
    {
        public UserEquipmentNotFoundToUnassignException(Guid userId, Guid equipmentId) 
            : base(DomainErrorCode.InternalServerError, 
                  $"Could not find equipment with id {equipmentId} for user with id {userId} for unassigning")
        {
        }
    }
}
