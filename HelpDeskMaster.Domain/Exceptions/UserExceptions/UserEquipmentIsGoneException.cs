namespace HelpDeskMaster.Domain.Exceptions.UserExceptions
{
    public class UserEquipmentIsGoneException : DomainException
    {
        public UserEquipmentIsGoneException(Guid userId, Guid equipmentId) 
            : base(DomainErrorCode.Gone, $"User with id {userId} does not assigned with equipment id {equipmentId}.")
        {
        }
    }
}
