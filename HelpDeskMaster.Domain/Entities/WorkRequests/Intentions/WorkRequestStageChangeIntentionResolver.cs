using HelpDeskMaster.Domain.Authentication;
using HelpDeskMaster.Domain.Authorization;

namespace HelpDeskMaster.Domain.Entities.WorkRequests.Intentions
{
    internal class WorkRequestStageChangeIntentionResolver : IIntentionResolver<WorkRequestStageChangeIntention, WorkRequest>
    {
        public bool Resolve(IIdentity subject, WorkRequest workRequest,
            WorkRequestStageChangeIntention intention)
        {
            if (!subject.IsAuthenticated()) return false;

            return intention switch
            {
                // move next resolving
                WorkRequestStageChangeIntention.FromNewRequestToAssignment => resolveFromNewRequestToAssignment(subject),
                WorkRequestStageChangeIntention.FromAssignmentToInWork => resolveFromAssignmentToInWork(subject),
                WorkRequestStageChangeIntention.FromInWorkToDone => resolveFromInWorkToDone(subject, workRequest),
                WorkRequestStageChangeIntention.FromDoneToArhive => resolveFromDoneToArhive(subject, workRequest),

                // move back resolving
                WorkRequestStageChangeIntention.FromArhiveToInWork => resolveFromArhiveToInWork(subject, workRequest),
                WorkRequestStageChangeIntention.FromInWorkToAssignment => resolveFromInWorkToAssignment(subject, workRequest),
                WorkRequestStageChangeIntention.FromDoneToInWork => resolveFromDoneToInWork(subject, workRequest),

                _ => false
            };
        }

        // move next resolving
        private bool resolveFromNewRequestToAssignment(IIdentity subject) =>
            subject.IsAdmin() || subject.IsHelpDeskMember();

        private bool resolveFromAssignmentToInWork(IIdentity subject) =>
            subject.IsAdmin() || subject.IsHelpDeskMember();

        private bool resolveFromInWorkToDone(IIdentity subject, WorkRequest workRequest) =>
            subject.IsAdmin() || subject.IsHelpDeskMember() && subject.UserId == workRequest.ExecuterId;

        private bool resolveFromDoneToArhive(IIdentity subject, WorkRequest workRequest) =>
            subject.IsAdmin() || subject.UserId == workRequest.AuthorId;

        // move back resolving
        private bool resolveFromArhiveToInWork(IIdentity subject, WorkRequest workRequest) =>
            subject.IsAdmin() || subject.IsHelpDeskMember() && subject.UserId == workRequest.ExecuterId;

        private bool resolveFromInWorkToAssignment(IIdentity subject, WorkRequest workRequest) =>
            subject.IsAdmin() || subject.IsHelpDeskMember() && subject.UserId == workRequest.ExecuterId;

        private bool resolveFromDoneToInWork(IIdentity subject, WorkRequest workRequest) =>
            subject.IsAdmin() ||
            subject.UserId == workRequest.AuthorId ||
            subject.IsHelpDeskMember() && subject.UserId == workRequest.ExecuterId;
    }
}