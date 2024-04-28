using HelpDeskMaster.Domain.Entities.Equipments;

namespace HelpDeskMaster.Persistence.Data.Repositories.Equipment
{
    internal class EquipmentRepository : IEquipmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EquipmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Entities.Equipments.Equipment?> GetEquipmentByIdAsync(
            Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Equipments.FindAsync(id, cancellationToken);
        }

        public async Task DeleteAsync(Guid equipmentId, CancellationToken cancellationToken)
        {
            var equipment = await _dbContext.Equipments.FindAsync(equipmentId, cancellationToken);

            if(equipment == null)
            {
                return;
            }

            _dbContext.Equipments.Remove(equipment);
        }

        public async Task InsertAsync(Domain.Entities.Equipments.Equipment equipment, CancellationToken cancellationToken)
        {
            await _dbContext.Equipments.AddAsync(equipment, cancellationToken);
        }

        public void Update(Domain.Entities.Equipments.Equipment equipment)
        {
            _dbContext.Equipments.Update(equipment);
        }
    }
}
