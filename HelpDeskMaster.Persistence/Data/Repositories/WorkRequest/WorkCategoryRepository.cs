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

        public async Task Delete(Guid workCategoryId)
        {
            var workCategory = await _dbContext.WorkCategories.FindAsync(workCategoryId);

            if(workCategory != null)
            {
                _dbContext.Remove(workCategory);
            }
        }

        public Task<bool> IsWorkCategoryUsedAsync(Guid workCategoryId, CancellationToken cancellationToken)
        {
            // TODO: add select from work requests by workCategoryId
            return Task.FromResult(false);
        }
    }
}
