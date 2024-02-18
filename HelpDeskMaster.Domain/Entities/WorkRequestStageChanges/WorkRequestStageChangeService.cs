using HelpDeskMaster.Domain.Authorization;
using HelpDeskMaster.Domain.Entities.WorkRequests;
using HelpDeskMaster.Domain.Entities.WorkRequestStageChanges.Intentions;
using HelpDeskMaster.Domain.Exceptions.WorkRequestExceptions;

namespace HelpDeskMaster.Domain.Entities.WorkRequestStageChanges
{
    public class WorkRequestStageChangeService
    {
        private readonly IIntentionManager _intentionManager;

        public WorkRequestStageChangeService(IIntentionManager intentionManager)
        {
            _intentionManager = intentionManager;
        }

        public WorkRequestStageChange MoveRequestStageNext(WorkRequest workRequest)
        {
            if (workRequest.RequestStageChanges.Count == 0)
            {
                throw new WorkRequestStageChangesHistoryGoneException(workRequest.Id);
            }

            var lastStage = workRequest.RequestStageChanges
                .OrderByDescending(x => x.CreatedAt).First();

            if (!_stagesToMoveNext.TryGetValue(lastStage.Stage, out var instruction))
            {
                throw new WorkRequestNextStageResolvingException(workRequest.Id);
            }

            _intentionManager.ThrowIfForbidden(
                instruction.Intention, workRequest);

            return workRequest.ChangeRequestStage(instruction.StageTo);
        }

        public WorkRequestStageChange MoveRequestStageBack(WorkRequest workRequest)
        {
            if (workRequest.RequestStageChanges.Count == 0)
            {
                throw new WorkRequestStageChangesHistoryGoneException(workRequest.Id);
            }

            var lastStage = workRequest.RequestStageChanges
                .OrderByDescending(x => x.CreatedAt).First();

            if (!_stagesToMoveBack.TryGetValue(lastStage.Stage, out var instruction))
            {
                throw new WorkRequestPreviousStageResolvingException(workRequest.Id);
            }

            _intentionManager.ThrowIfForbidden(
                instruction.Intention, workRequest);

            return workRequest.ChangeRequestStage(instruction.StageTo);
        }

        private readonly Dictionary<WorkRequestStage, WorkRequestStageChangeInstruction> _stagesToMoveNext =
            new Dictionary<WorkRequestStage, WorkRequestStageChangeInstruction>
            {
                {
                    // NewRequest -> Assignment
                    WorkRequestStage.NewRequest,
                    new WorkRequestStageChangeInstruction(
                        WorkRequestStage.Assignment,
                        WorkRequestStageChangeIntention.FromNewRequestToAssignment)
                },
                { 
                    // Assignment -> InWork
                    WorkRequestStage.Assignment,
                    new WorkRequestStageChangeInstruction(
                        WorkRequestStage.InWork,
                        WorkRequestStageChangeIntention.FromAssignmentToInWork)
                },
                { 
                    // InWork -> Done
                    WorkRequestStage.InWork,
                    new WorkRequestStageChangeInstruction(
                        WorkRequestStage.Done,
                        WorkRequestStageChangeIntention.FromInWorkToDone)
                },
                { 
                    // Done -> Archive
                    WorkRequestStage.Done,
                    new WorkRequestStageChangeInstruction(
                        WorkRequestStage.Archive,
                        WorkRequestStageChangeIntention.FromDoneToArhive)
                }
            };

        private readonly Dictionary<WorkRequestStage, WorkRequestStageChangeInstruction> _stagesToMoveBack =
            new Dictionary<WorkRequestStage, WorkRequestStageChangeInstruction>
            {
                { 
                    // Archive -> InWork
                    WorkRequestStage.Archive,
                    new WorkRequestStageChangeInstruction(
                        WorkRequestStage.InWork,
                        WorkRequestStageChangeIntention.FromArhiveToInWork)
                },
                { 
                    // InWork -> Assignment
                    WorkRequestStage.InWork,
                    new WorkRequestStageChangeInstruction(
                        WorkRequestStage.Assignment,
                        WorkRequestStageChangeIntention.FromInWorkToAssignment)
                },
                {
                    // Done -> InWork
                    WorkRequestStage.Done,
                    new WorkRequestStageChangeInstruction(
                        WorkRequestStage.InWork,
                        WorkRequestStageChangeIntention.FromDoneToInWork)
                }
            };

        private class WorkRequestStageChangeInstruction
        {
            public WorkRequestStageChangeInstruction(WorkRequestStage stageTo,
                WorkRequestStageChangeIntention intention)
            {
                StageTo = stageTo;
                Intention = intention;
            }

            public WorkRequestStage StageTo { get; }

            public WorkRequestStageChangeIntention Intention { get; }
        }
    }
}