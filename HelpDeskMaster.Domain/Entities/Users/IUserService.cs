
namespace HelpDeskMaster.Domain.Entities.Users
{
    public interface IUserService
    {
        Task<User> GetUserByLoginAsync(string login, CancellationToken cancellationToken);
    }
}