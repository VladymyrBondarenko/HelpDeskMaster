using HelpDeskMaster.Domain.Entities.Equipments;
using HelpDeskMaster.Domain.Entities.EquipmentTypes;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskMaster.Persistence.Data.Repositories.Equipment
{
    internal class ComputerEquipmentRepository : IComputerEquipmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ComputerEquipmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InsertAsync(ComputerEquipment computerEquipment, 
            CancellationToken cancellationToken)
        {
            await _dbContext.ComputerEquipments.AddAsync(computerEquipment, cancellationToken);
        }

        public async Task<bool> IsEquipmentAssignedAsync(Guid equipmentId, DateTimeOffset assignDate, 
            CancellationToken cancellationToken)
        {
            return await _dbContext.ComputerEquipments.AnyAsync(x =>
                x.EquipmentId == equipmentId
                && (x.UnassignedDate == null || x.UnassignedDate >= assignDate),
                cancellationToken);
        }

        public async Task<bool> IsEquipmentComputerAsync(
            Guid equipmentId, CancellationToken cancellationToken)
        {
            var equipment = await _dbContext.Equipments
                .Include(x => x.EquipmentType)
                .FirstOrDefaultAsync(x => x.Id == equipmentId, cancellationToken);

            if(equipment == null)
            {
                return false;
            }

            return equipment.EquipmentType?.TypeOfEquipment == TypeOfEquipment.PC;
        }
    }
}
