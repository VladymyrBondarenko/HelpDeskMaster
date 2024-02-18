namespace HelpDeskMaster.WebApi.Contracts.WorkRequest.Responses
{
    public class WorkDirectionModel
    {
        public Guid Id { get; init; }

        public string? Title { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
