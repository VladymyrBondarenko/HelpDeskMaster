namespace HelpDeskMaster.Domain.Exceptions.WorkRequestExceptions
{
    public class WorkRequestIsGoneException : DomainException
    {
        public WorkRequestIsGoneException(Guid Id) 
            : base(DomainErrorCode.Gone, $"Work request with id {Id} is gone")
        {
        }
    }
}
