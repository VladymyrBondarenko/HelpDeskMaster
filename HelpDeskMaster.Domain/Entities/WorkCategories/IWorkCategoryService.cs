
namespace HelpDeskMaster.Domain.Entities.WorkCategories
{
    public interface IWorkCategoryService
    {
        Task<WorkCategory> CreateWorkCategoryAsync(string title, CancellationToken cancellationToken);
        Task DeleteWorkCategoryAsync(Guid workCategoryId, CancellationToken cancellationToken);
    }
}