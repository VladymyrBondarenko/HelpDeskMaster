namespace HelpDeskMaster.Domain.Exceptions.WorkRequestExceptions
{
    public class WorkRequestNextStageResolvingException : DomainException
    {
        public WorkRequestNextStageResolvingException(Guid requestId) 
            : base(DomainErrorCode.InternalServerError, $"Could not resolve next stage for request with id {requestId}")
        {
        }
    }
}
