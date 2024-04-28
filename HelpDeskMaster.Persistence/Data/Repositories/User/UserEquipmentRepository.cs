using HelpDeskMaster.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskMaster.Persistence.Data.Repositories.User
{
    internal class UserEquipmentRepository : IUserEquipmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserEquipmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InsertAsync(UserEquipment userEquipment, CancellationToken cancellationToken)
        {
            await _dbContext.AddAsync(userEquipment, cancellationToken);
        }

        public async Task<bool> IsEquipmentAssignedAsync(
            Guid equipmentId, DateTimeOffset assignDate, 
            CancellationToken cancellationToken)
        {
            return await _dbContext.UserEquipments.AnyAsync(x => 
                x.EquipmentId == equipmentId
                && (x.UnassignedDate == null || x.UnassignedDate >= assignDate), 
                cancellationToken);
        }

        public async Task<bool> IsEquipmentAssignedToUserAsync(
            Guid equipmentId, Guid userId, DateTimeOffset date, 
            CancellationToken cancellationToken)
        {
            var equipmentAssignedToUser = await _dbContext.UserEquipments.SingleOrDefaultAsync(x => 
                x.EquipmentId == equipmentId && x.UserId == userId 
                && x.AssignedDate <= date
                && (x.UnassignedDate == null || x.UnassignedDate >= date),
                cancellationToken);

            return equipmentAssignedToUser != null;
        }
    }
}
