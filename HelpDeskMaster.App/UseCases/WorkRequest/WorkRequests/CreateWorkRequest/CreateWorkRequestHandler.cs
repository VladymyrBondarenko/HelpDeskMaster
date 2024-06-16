using HelpDeskMaster.Domain.Entities.WorkRequests;
using HelpDeskMaster.Persistence.Data.Repositories;
using MediatR;

namespace HelpDeskMaster.App.UseCases.WorkRequest.WorkRequests.CreateWorkRequest
{
    internal class CreateWorkRequestHandler : IRequestHandler<CreateWorkRequestCommand, Domain.Entities.WorkRequests.WorkRequest>
    {
        private readonly IWorkRequestRepository _workRequestRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateWorkRequestHandler(
            IWorkRequestRepository workRequestRepository,
            IUnitOfWork unitOfWork)
        {
            _workRequestRepository = workRequestRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Domain.Entities.WorkRequests.WorkRequest> Handle(
            CreateWorkRequestCommand request, 
            CancellationToken cancellationToken)
        {
            var newWorkRequest = Domain.Entities.WorkRequests.WorkRequest.Create(
                Guid.NewGuid(), 
                DateTimeOffset.UtcNow,
                request.AuthorId, 
                request.WorkDirectionId, 
                request.WorkCategoryId, 
                request.FailureRevealedDate, 
                request.DesiredExecutionDate, 
                request.Content);

            await _workRequestRepository.InsertAsync(newWorkRequest, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return newWorkRequest;
        }
    }
}
