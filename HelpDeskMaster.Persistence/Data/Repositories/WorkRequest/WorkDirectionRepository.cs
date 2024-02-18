using HelpDeskMaster.Domain.Entities.WorkDirections;

namespace HelpDeskMaster.Persistence.Data.Repositories.WorkRequest
{
    internal class WorkDirectionRepository : IWorkDirectionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public WorkDirectionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(Guid workDirectionId)
        {
            _dbContext.Remove(workDirectionId);
        }

        public async Task InsertAsync(WorkDirection workDirection, CancellationToken cancellationToken)
        {
            await _dbContext.AddAsync(workDirection, cancellationToken);
        }

        public Task<bool> IsWorkDirectionUsed(Guid workDirectionId, CancellationToken cancellationToken)
        {
            // TODO: add select from work requests by workDirectionId
            return Task.FromResult(true);
        }
    }
}
