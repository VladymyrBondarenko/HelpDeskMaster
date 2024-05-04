namespace HelpDeskMaster.WebApi.Contracts.User.Responses
{
    public record GetUserByLoginResponse(
        Guid Id,
        string Login,
        string? PhoneNumber,
        DateTimeOffset CreatedAt,
        DateTimeOffset? UpdatedAt)
    {
    }
}
