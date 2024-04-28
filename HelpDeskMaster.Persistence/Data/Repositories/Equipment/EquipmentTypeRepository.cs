using HelpDeskMaster.Domain.Entities.EquipmentTypes;

namespace HelpDeskMaster.Persistence.Data.Repositories.Equipment
{
    internal class EquipmentTypeRepository : IEquipmentTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EquipmentTypeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task DeleteAsync(Guid equipmentTypeId, CancellationToken cancellationToken)
        {
            var equipmentType = await _dbContext.EquipmentTypes.FindAsync(equipmentTypeId, cancellationToken);

            if (equipmentType == null)
            {
                return;
            }

            _dbContext.EquipmentTypes.Remove(equipmentType);
        }

        public async Task InsertAsync(EquipmentType equipmentType, CancellationToken cancellationToken)
        {
            await _dbContext.EquipmentTypes.AddAsync(equipmentType, cancellationToken);
        }

        public void Update(EquipmentType equipmentType)
        {
            _dbContext.EquipmentTypes.Update(equipmentType);
        }
    }
}
