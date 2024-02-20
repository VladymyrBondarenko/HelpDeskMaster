namespace HelpDeskMaster.WebApi.Contracts.WorkRequest.Responses
{
    public class GetAllWorkCategoriesResponse
    {
        public required IReadOnlyList<WorkCategoryModel> WorkCategories { get; set; }
    }
}
