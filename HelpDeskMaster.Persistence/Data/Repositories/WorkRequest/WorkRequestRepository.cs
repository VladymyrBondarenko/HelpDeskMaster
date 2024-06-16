using HelpDeskMaster.Domain.Entities.WorkRequests;

namespace HelpDeskMaster.Persistence.Data.Repositories.WorkRequest
{
    internal class WorkRequestRepository : IWorkRequestRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public WorkRequestRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InsertAsync(Domain.Entities.WorkRequests.WorkRequest workRequest, CancellationToken cancellationToken)
        {
            await _dbContext.WorkRequests.AddAsync(workRequest, cancellationToken);
        }
    }
}
