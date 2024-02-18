using HelpDeskMaster.Domain.Entities.WorkDirections;
using HelpDeskMaster.Persistence.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskMaster.App.UseCases.WorkRequest.WorkDirections.GetAllWorkDirections
{
    internal class GetAllWorkDirectionsQueryHandler : IRequestHandler<GetAllWorkDirectionsQuery, List<WorkDirection>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAllWorkDirectionsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<WorkDirection>> Handle(GetAllWorkDirectionsQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.WorkDirections.ToListAsync(cancellationToken);
        }
    }
}
