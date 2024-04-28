using HelpDeskMaster.Domain.Exceptions.EquipmentExceptions;
using HelpDeskMaster.Persistence.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskMaster.App.UseCases.Equipment.Equipments.GetEquipmentById
{
    internal class GetEquipmentByIdQueryHandler : IRequestHandler<GetEquipmentByIdQuery, Domain.Entities.Equipments.Equipment>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetEquipmentByIdQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Entities.Equipments.Equipment> Handle(
            GetEquipmentByIdQuery request, CancellationToken cancellationToken)
        {
            var equipment = await _dbContext.Equipments
                    .Include(x => x.EquipmentType)
                    .Include(x => x.EquipmentComputerInfo)
                    .Include(x => x.ComputerEquipments)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if(equipment == null)
            {
                throw new EquipmentIsGoneException(request.Id);
            }

            return equipment;
        }
    }
}
