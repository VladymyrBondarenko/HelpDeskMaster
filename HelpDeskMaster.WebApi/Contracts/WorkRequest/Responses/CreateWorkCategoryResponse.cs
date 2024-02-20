namespace HelpDeskMaster.WebApi.Contracts.WorkRequest.Responses
{
    public class CreateWorkCategoryResponse
    {
        public Guid Id { get; init; }

        public required string Title { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
