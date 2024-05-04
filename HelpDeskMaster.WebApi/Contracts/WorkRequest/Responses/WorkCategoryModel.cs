namespace HelpDeskMaster.WebApi.Contracts.WorkRequest.Responses
{
    public record WorkCategoryModel(
        Guid Id,
        string Title,
        DateTimeOffset CreatedAt,
        DateTimeOffset? UpdatedAt)
    {
    }
}
