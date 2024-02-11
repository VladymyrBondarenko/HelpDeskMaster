namespace HelpDeskMaster.Domain.Exceptions.WorkRequestExceptions
{
    public class WorkRequestEquipmentIsGoneException : DomainException
    {
        public WorkRequestEquipmentIsGoneException(Guid workRequestId, Guid workRequestEquipmentId) 
            : base(DomainErrorCode.Gone, 
                  $"Work request with id {workRequestId} does not contain work request equipment with id {workRequestEquipmentId}")
        {
        }
    }
}
