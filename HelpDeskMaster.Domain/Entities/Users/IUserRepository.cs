namespace HelpDeskMaster.Domain.Entities.Users
{
    public interface IUserRepository
    {
        Task<User?> GetAsync(string login, CancellationToken cancellationToken);

        Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);

        Task InsertAsync(User user, CancellationToken cancellationToken);

        void Update(User user);
    }
}