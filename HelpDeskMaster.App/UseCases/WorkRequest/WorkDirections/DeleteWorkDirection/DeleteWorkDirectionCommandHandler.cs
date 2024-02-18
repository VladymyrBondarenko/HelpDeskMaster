using HelpDeskMaster.Domain.Entities.WorkDirections;
using HelpDeskMaster.Persistence.Data.Repositories;
using MediatR;

namespace HelpDeskMaster.App.UseCases.WorkRequest.WorkDirections.DeleteWorkDirection
{
    internal class DeleteWorkDirectionCommandHandler : IRequestHandler<DeleteWorkDirectionCommand, bool>
    {
        private readonly IWorkDirectionService _workDirectionService;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWorkDirectionCommandHandler(IWorkDirectionService workDirectionService,
            IUnitOfWork unitOfWork)
        {
            _workDirectionService = workDirectionService;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteWorkDirectionCommand request, CancellationToken cancellationToken)
        {
            await _workDirectionService.DeleteWorkDirectionAsync(request.WorkDirectionId, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
