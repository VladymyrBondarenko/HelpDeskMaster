namespace HelpDeskMaster.WebApi.Contracts.WorkRequest.Responses
{
    public record GetAllWorkCategoriesResponse(
        IReadOnlyList<WorkCategoryModel> WorkCategories)
    {
    }
}
