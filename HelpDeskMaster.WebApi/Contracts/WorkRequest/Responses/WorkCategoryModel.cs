namespace HelpDeskMaster.WebApi.Contracts.WorkRequest.Responses
{
    public class WorkCategoryModel
    {
        public Guid Id { get; set; }

        public required string Title { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
