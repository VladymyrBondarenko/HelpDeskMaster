namespace HelpDeskMaster.Domain.Exceptions.WorkRequestExceptions
{
    public class WorkRequestPreviousStageResolvingException : DomainException
    {
        public WorkRequestPreviousStageResolvingException(Guid requestId) 
            : base(DomainErrorCode.InternalServerError, $"Could not resolve previous stage for request with id {requestId}")
        {
        }
    }
}
