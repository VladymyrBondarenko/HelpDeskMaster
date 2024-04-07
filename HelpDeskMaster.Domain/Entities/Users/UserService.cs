using HelpDeskMaster.Domain.Authorization;
using HelpDeskMaster.Domain.Entities.Users.Intentions;
using HelpDeskMaster.Domain.Exceptions.UserExceptions;

namespace HelpDeskMaster.Domain.Entities.Users
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IIntentionManager _intentionManager;

        public UserService(IUserRepository userRepository,
            IIntentionManager intentionManager)
        {
            _userRepository = userRepository;
            _intentionManager = intentionManager;
        }

        public async Task<User> GetUserByLoginAsync(string login, CancellationToken cancellationToken)
        {
            await _intentionManager.ThrowIfForbiddenAsync(ManageUserIntention.GetUserByLogin, 
                cancellationToken);

            var user = await _userRepository.GetAsync(login, cancellationToken);

            if(user == null)
            {
                throw new UserIsGoneException(login);
            }

            return user;
        }
    }
}
