using HelpDeskMaster.Domain.Entities.WorkRequestStageChanges;

namespace HelpDeskMaster.Domain.Exceptions.WorkRequestExceptions
{
    public class WorkRequestAssigningExecutorToRequestStageException : DomainException
    {
        public WorkRequestAssigningExecutorToRequestStageException(Guid executerId, WorkRequestStage stage) 
            : base(DomainErrorCode.Forbidden, 
                  $"Not possible to assign executer with id {executerId} at the stage {stage}")
        {
        }
    }
}
