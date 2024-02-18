using HelpDeskMaster.Domain.Authorization;
using HelpDeskMaster.Domain.Entities.WorkCategories.Intentions;
using HelpDeskMaster.Domain.Exceptions.WorkRequestExceptions;

namespace HelpDeskMaster.Domain.Entities.WorkCategories
{
    internal class WorkCategoryService : IWorkCategoryService
    {
        private readonly IIntentionManager _intentionManager;
        private readonly IWorkCategoryRepository _workCategoryRepository;

        public WorkCategoryService(IIntentionManager intentionManager,
            IWorkCategoryRepository workCategoryRepository)
        {
            _intentionManager = intentionManager;
            _workCategoryRepository = workCategoryRepository;
        }

        public async Task<WorkCategory> CreateWorkCategoryAsync(string title, CancellationToken cancellationToken)
        {
            _intentionManager.ThrowIfForbidden(ManageWorkCategoryIntention.Create);

            var workCategory = new WorkCategory(Guid.NewGuid(), DateTimeOffset.UtcNow, title);

            await _workCategoryRepository.InsertAsync(workCategory, cancellationToken);

            return workCategory;
        }

        public async Task DeleteWorkCategoryAsync(Guid workCategoryId, CancellationToken cancellationToken)
        {
            _intentionManager.ThrowIfForbidden(ManageWorkCategoryIntention.Delete);

            if (await _workCategoryRepository.IsWorkCategoryUsedAsync(workCategoryId, cancellationToken))
            {
                throw new WorkCategoryIsUsedException(workCategoryId);
            }

            _workCategoryRepository.Delete(workCategoryId);
        }
    }
}