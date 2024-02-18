using HelpDeskMaster.Domain.Entities.WorkCategories;

namespace HelpDeskMaster.Persistence.Data.Repositories.WorkRequest
{
    internal class WorkCategoryRepository : IWorkCategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public WorkCategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InsertAsync(WorkCategory workCategory, CancellationToken cancellationToken)
        {
            await _dbContext.AddAsync(workCategory, cancellationToken);
        }

        public void Delete(Guid workCategoryId)
        {
            _dbContext.Remove(workCategoryId);
        }

        public Task<bool> IsWorkCategoryUsedAsync(Guid workCategoryId, CancellationToken cancellationToken)
        {
            // TODO: add select from work requests by workCategoryId
            return Task.FromResult(true);
        }
    }
}
