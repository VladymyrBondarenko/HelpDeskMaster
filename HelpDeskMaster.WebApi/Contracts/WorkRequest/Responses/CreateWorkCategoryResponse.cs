namespace HelpDeskMaster.WebApi.Contracts.WorkRequest.Responses
{
    public record CreateWorkCategoryResponse(
        Guid Id,
        string Title,
        DateTimeOffset CreatedAts)
    {
    }
}
