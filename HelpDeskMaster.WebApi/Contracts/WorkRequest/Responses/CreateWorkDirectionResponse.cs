namespace HelpDeskMaster.WebApi.Contracts.WorkRequest.Responses
{
    public record CreateWorkDirectionResponse(
        Guid Id,
        string Title,
        DateTimeOffset CreatedAt)
    {
    }
}