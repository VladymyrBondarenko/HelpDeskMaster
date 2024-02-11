namespace HelpDeskMaster.Domain.Exceptions.WorkRequestExceptions
{
    public class WorkRequestStageChangesHistoryGoneException : DomainException
    {
        public WorkRequestStageChangesHistoryGoneException(Guid requestId) 
            : base(DomainErrorCode.Gone, $"The history of stage changes of the request with id {requestId} is gone")
        {
        }
    }
}
