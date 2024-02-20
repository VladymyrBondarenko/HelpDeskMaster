namespace HelpDeskMaster.Domain.Entities.WorkCategories
{
    public interface IWorkCategoryRepository
    {
        Task InsertAsync(WorkCategory workCategory, CancellationToken cancellationToken);

        Task Delete(Guid workCategoryId);

        Task<bool> IsWorkCategoryUsedAsync(Guid workCategoryId, CancellationToken cancellationToken);
    }
}