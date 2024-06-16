namespace HelpDeskMaster.WebApi.Contracts.WorkRequest.Responses
{
    public record CreateWorkRequestResponse(
        Guid Id,
        Guid AuthorId,
        Guid WorkDirectionId,
        Guid WorkCategoryId,
        DateTimeOffset FailureRevealedDate,
        DateTimeOffset DesiredExecutionDate,
        string? Content,
        DateTimeOffset CreatedAt)
    {
    }
}
