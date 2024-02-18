namespace HelpDeskMaster.Domain.Exceptions.WorkRequestExceptions
{
    public class WorkCategoryIsUsedException : DomainException
    {
        public WorkCategoryIsUsedException(Guid workCategoryId) 
            : base(DomainErrorCode.InternalServerError, 
                  $"Work category with id {workCategoryId} cannot be deleted, because it's already used in work requests")
        {
        }
    }
}
