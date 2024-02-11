using HelpDeskMaster.Domain.Entities.WorkRequests;

namespace HelpDeskMaster.Domain.Exceptions.WorkRequestExceptions
{
    public class WorkRequestUnassigningExecutorToRequestStageException : DomainException
    {
        public WorkRequestUnassigningExecutorToRequestStageException(Guid? executerId, WorkRequestStage stage)
            : base(DomainErrorCode.Forbidden, 
                  $"Not possible to unassign executer with id {executerId} at the stage {stage}")
        {
        }
    }
}