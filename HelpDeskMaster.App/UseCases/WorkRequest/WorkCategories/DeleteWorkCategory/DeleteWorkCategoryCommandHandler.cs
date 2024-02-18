using HelpDeskMaster.Domain.Entities.WorkCategories;
using HelpDeskMaster.Persistence.Data.Repositories;
using MediatR;

namespace HelpDeskMaster.App.UseCases.WorkRequest.WorkCategories.DeleteWorkCategory
{
    internal class DeleteWorkCategoryCommandHandler : IRequestHandler<DeleteWorkCategoryCommand, bool>
    {
        private readonly IWorkCategoryService _workCategoryService;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWorkCategoryCommandHandler(IWorkCategoryService workCategoryService,
            IUnitOfWork unitOfWork)
        {
            _workCategoryService = workCategoryService;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteWorkCategoryCommand request, CancellationToken cancellationToken)
        {
            await _workCategoryService.DeleteWorkCategoryAsync(request.WorkCategoryId, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
