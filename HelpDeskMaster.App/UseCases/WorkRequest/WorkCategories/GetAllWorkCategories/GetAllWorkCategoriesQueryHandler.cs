using HelpDeskMaster.Domain.Entities.WorkCategories;
using HelpDeskMaster.Persistence.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskMaster.App.UseCases.WorkRequest.WorkCategories.GetAllWorkCategories
{
    internal class GetAllWorkCategoriesQueryHandler : IRequestHandler<GetAllWorkCategoriesQuery, List<WorkCategory>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAllWorkCategoriesQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<WorkCategory>> Handle(GetAllWorkCategoriesQuery _, CancellationToken cancellationToken)
        {
            return await _dbContext.WorkCategories.ToListAsync(cancellationToken);
        }
    }
}
