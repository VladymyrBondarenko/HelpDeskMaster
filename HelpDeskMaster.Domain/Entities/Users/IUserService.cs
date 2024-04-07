
namespace HelpDeskMaster.Domain.Entities.Users
{
    public interface IUserService
    {
        Task CreateOrUpdateUserAsync(User user, CancellationToken cancellationToken);
    }
}