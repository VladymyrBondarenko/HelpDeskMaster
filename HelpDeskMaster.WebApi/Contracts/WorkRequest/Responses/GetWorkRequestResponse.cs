namespace HelpDeskMaster.WebApi.Contracts.WorkRequest.Responses
{
    public record GetWorkRequestResponse(
        Guid Id,
        Guid AuthorId,
        Guid WorkDirectionId,
        WorkDirectionModel WorkDirection,
        Guid WorkCategoryId,
        WorkCategoryModel WorkCategory,
        DateTimeOffset FailureRevealedDate,
        DateTimeOffset DesiredExecutionDate,
        string? Content,
        DateTimeOffset CreatedAt)
    {
    }
}
