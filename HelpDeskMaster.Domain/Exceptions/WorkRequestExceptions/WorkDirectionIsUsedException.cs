namespace HelpDeskMaster.Domain.Exceptions.WorkRequestExceptions
{
    public class WorkDirectionIsUsedException : DomainException
    {
        public WorkDirectionIsUsedException(Guid workDirectionId) 
            : base(DomainErrorCode.InternalServerError,
                  $"Work direction with id {workDirectionId} cannot be deleted, because it's already used in work requests")
        {
        }
    }
}
