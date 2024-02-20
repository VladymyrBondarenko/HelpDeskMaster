using HelpDeskMaster.Domain.Entities.WorkCategories;
using HelpDeskMaster.Persistence.Data.Repositories;
using MediatR;

namespace HelpDeskMaster.App.UseCases.WorkRequest.WorkCategories.CreateWorkCategory
{
    internal class CreateWorkCategoryCommandHandler : IRequestHandler<CreateWorkCategoryCommand, WorkCategory>
    {
        private readonly IWorkCategoryService _workCategoryService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateWorkCategoryCommandHandler(IWorkCategoryService workCategoryService,
            IUnitOfWork unitOfWork)
        {
            _workCategoryService = workCategoryService;
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkCategory> Handle(CreateWorkCategoryCommand request, CancellationToken cancellationToken)
        {
            var workCategory = await _workCategoryService.CreateWorkCategoryAsync(request.Title, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return workCategory;
        }
    }
}
