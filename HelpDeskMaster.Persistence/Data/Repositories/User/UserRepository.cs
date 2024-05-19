using HelpDeskMaster.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskMaster.Persistence.Data.Repositories.User
{
    internal class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Entities.Users.User?> GetAsync(string login, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                .Include(x => x.Equipments)
                    .ThenInclude(x => x.Equipment)
                .FirstOrDefaultAsync(x => x.Login.Value == login, cancellationToken);
        }

        public async Task<Domain.Entities.Users.User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                .Include(x => x.Equipments)
                    .ThenInclude(x => x.Equipment)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task InsertAsync(Domain.Entities.Users.User user, CancellationToken cancellationToken)
        {
            await _dbContext.Users.AddAsync(user, cancellationToken);
        }

        public void Update(Domain.Entities.Users.User user)
        {
            _dbContext.Users.Update(user);
        }
    }
}
