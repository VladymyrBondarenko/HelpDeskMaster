using HelpDeskMaster.Domain.Entities.Equipments;

namespace HelpDeskMaster.Persistence.Data.Repositories.Equipment
{
    internal class EquipmentComputerInfoRepository : IEquipmentComputerInfoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EquipmentComputerInfoRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InsertAsync(EquipmentComputerInfo computerInfo, CancellationToken cancellationToken)
        {
            await _dbContext.EquipmentComputerInfos.AddAsync(computerInfo, cancellationToken);
        }
    }
}
