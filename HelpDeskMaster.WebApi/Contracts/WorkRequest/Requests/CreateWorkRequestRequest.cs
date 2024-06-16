namespace HelpDeskMaster.WebApi.Contracts.WorkRequest.Requests
{
    public record CreateWorkRequestRequest(
        Guid AuthorId,
        Guid WorkDirectionId,
        Guid WorkCategoryId,
        DateTimeOffset FailureRevealedDate,
        DateTimeOffset DesiredExecutionDate,
        string? Content)
    {
    }
}