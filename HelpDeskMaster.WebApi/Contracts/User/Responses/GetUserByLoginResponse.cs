namespace HelpDeskMaster.WebApi.Contracts.User.Responses
{
    public class GetUserByLoginResponse
    {
        public Guid Id { get; set; }

        public required string Login { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
