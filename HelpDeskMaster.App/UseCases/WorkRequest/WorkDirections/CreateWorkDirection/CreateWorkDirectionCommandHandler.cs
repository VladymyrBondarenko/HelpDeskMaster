using HelpDeskMaster.Domain.Entities.WorkDirections;
using HelpDeskMaster.Persistence.Data.Repositories;
using MediatR;

namespace HelpDeskMaster.App.UseCases.WorkRequest.WorkDirections.CreateWorkDirection
{
    internal class CreateWorkDirectionCommandHandler : IRequestHandler<CreateWorkDirectionCommand, Guid>
    {
        private readonly IWorkDirectionService _workDirectionService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateWorkDirectionCommandHandler(IWorkDirectionService workDirectionService,
            IUnitOfWork unitOfWork)
        {
            _workDirectionService = workDirectionService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(
            CreateWorkDirectionCommand request, CancellationToken cancellationToken)
        {
            var workDirection = await _workDirectionService.CreateWorkDirectionAsync(request.Title, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return workDirection.Id;
        }
    }
}
