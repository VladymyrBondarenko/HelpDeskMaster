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
            _intentionManager.ThrowIfForbidden(ManageWorkDirectionIntention.Create);

            var workDirection = new WorkDirection(
                Guid.NewGuid(), DateTimeOffset.UtcNow, title);

            await _workDirectionRepository.InsertAsync(workDirection, cancellationToken);

            return workDirection;
        }

        public async Task DeleteWorkDirectionAsync(Guid workDirectionId, CancellationToken cancellationToken)
        {
            _intentionManager.ThrowIfForbidden(ManageWorkDirectionIntention.Delete);

            if (await _workDirectionRepository.IsWorkDirectionUsed(workDirectionId, cancellationToken))
            {
                throw new WorkDirectionIsUsedException(workDirectionId);
            }

            await _workDirectionRepository.Delete(workDirectionId);
        }
    }
}