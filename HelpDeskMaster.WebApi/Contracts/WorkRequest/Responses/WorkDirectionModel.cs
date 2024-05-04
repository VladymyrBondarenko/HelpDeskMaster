namespace HelpDeskMaster.WebApi.Contracts.WorkRequest.Responses
{
    public record WorkDirectionModel(
        Guid Id,
        string Title,
        DateTimeOffset CreatedAt,
        DateTimeOffset? UpdatedAt)
    {
    }
}
