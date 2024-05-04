using HelpDeskMaster.Domain.Authorization;
using HelpDeskMaster.Domain.Entities.WorkDirections.Intentions;
using HelpDeskMaster.Domain.Exceptions.WorkRequestExceptions;

namespace HelpDeskMaster.Domain.Entities.WorkDirections
{
    internal class WorkDirectionService : IWorkDirectionService
    {
        private readonly IIntentionManager _intentionManager;
        private readonly IWorkDirectionRepository _workDirectionRepository;

        public WorkDirectionService(IIntentionManager intentionManager,
            IWorkDirectionRepository workDirectionRepository)
        {
            _intentionManager = intentionManager;
            _workDirectionRepository = workDirectionRepository;
        }

        public async Task<WorkDirection> CreateWorkDirectionAsync(string title, CancellationToken cancellationToken)
        {
            await _intentionManager.ThrowIfForbiddenAsync(ManageWorkDirectionIntention.Create, 
                cancellationToken);

            var workDirection = new WorkDirection(
                Guid.NewGuid(), title, DateTimeOffset.UtcNow);

            await _workDirectionRepository.InsertAsync(workDirection, cancellationToken);

            return workDirection;
        }

        public async Task DeleteWorkDirectionAsync(Guid workDirectionId, CancellationToken cancellationToken)
        {
            await _intentionManager.ThrowIfForbiddenAsync(ManageWorkDirectionIntention.Delete, 
                cancellationToken);

            if (await _workDirectionRepository.IsWorkDirectionUsed(workDirectionId, cancellationToken))
            {
                throw new WorkDirectionIsUsedException(workDirectionId);
            }

            await _workDirectionRepository.Delete(workDirectionId);
        }
    }
}