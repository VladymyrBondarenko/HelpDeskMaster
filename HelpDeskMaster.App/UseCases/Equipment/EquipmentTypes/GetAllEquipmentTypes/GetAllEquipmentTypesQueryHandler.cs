using HelpDeskMaster.Domain.Entities.EquipmentTypes;
using HelpDeskMaster.Persistence.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskMaster.App.UseCases.Equipment.EquipmentTypes.GetAllEquipmentTypes
{
    internal class GetAllEquipmentTypesQueryHandler : IRequestHandler<GetAllEquipmentTypesQuery, List<EquipmentType>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAllEquipmentTypesQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<EquipmentType>> Handle(GetAllEquipmentTypesQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.EquipmentTypes.ToListAsync(cancellationToken);
        }
    }
}
