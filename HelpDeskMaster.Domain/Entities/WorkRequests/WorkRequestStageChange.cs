using Ardalis.GuardClauses;
using HelpDeskMaster.Domain.Abstractions;

namespace HelpDeskMaster.Domain.Entities.WorkRequests
{
    public class WorkRequestStageChange : Entity
    {
        public WorkRequestStageChange(Guid id,
            DateTimeOffset createdAt,
            Guid workRequestId,
            WorkRequestStage stage) : base(id, createdAt)
        {
            WorkRequestId = Guard.Against.Default(workRequestId);
            Stage = Guard.Against.Null(stage);
        }

        public Guid WorkRequestId { get; private set; }

        public WorkRequestStage Stage { get; private set; }
    }
}