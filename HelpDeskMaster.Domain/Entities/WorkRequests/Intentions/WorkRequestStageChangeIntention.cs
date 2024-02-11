namespace HelpDeskMaster.Domain.Entities.WorkRequests.Intentions
{
    internal enum WorkRequestStageChangeIntention
    {
        // move next
        FromNewRequestToAssignment,
        FromAssignmentToInWork,
        FromInWorkToDone,
        FromDoneToArhive,

        // move back
        FromArhiveToInWork,
        FromInWorkToAssignment,
        FromDoneToInWork
    }
}