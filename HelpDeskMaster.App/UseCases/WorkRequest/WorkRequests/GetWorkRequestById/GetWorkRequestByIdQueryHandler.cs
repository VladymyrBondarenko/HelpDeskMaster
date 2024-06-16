using HelpDeskMaster.Domain.Exceptions.WorkRequestExceptions;
using HelpDeskMaster.Persistence.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskMaster.App.UseCases.WorkRequest.WorkRequests.GetWorkRequestById
{
    internal class GetWorkRequestByIdQueryHandler : IRequestHandler<GetWorkRequestByIdQuery, Domain.Entities.WorkRequests.WorkRequest>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetWorkRequestByIdQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Entities.WorkRequests.WorkRequest> Handle(
            GetWorkRequestByIdQuery request, 
            CancellationToken cancellationToken)
        {
            var workRequest = await _dbContext.WorkRequests
                .Include(x => x.WorkCategory)
                .Include(x => x.WorkDirection)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if(workRequest == null)
            {
                throw new WorkRequestIsGoneException(request.Id);
            }

            return workRequest;
        }
    }
}
