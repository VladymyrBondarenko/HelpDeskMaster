namespace HelpDeskMaster.Domain.Entities.Users
{
    public interface IUserRepository
    {
        Task<User?> GetAsync(string login, CancellationToken cancellationToken);

        public Task InsertAsync(User user, CancellationToken cancellationToken);

        public void Update(User user);
    }
}