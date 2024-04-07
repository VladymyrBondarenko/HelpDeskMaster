namespace HelpDeskMaster.Domain.Entities.Users
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateOrUpdateUserAsync(User user, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetAsync(user.Login.Value, cancellationToken);

            if (existingUser != null)
            {
                if(user.PhoneNumber != existingUser.PhoneNumber)
                {
                    user.UpdatePhoneNumber(existingUser.PhoneNumber);
                    _userRepository.Update(user);
                }
            }
            else
            {
                await _userRepository.InsertAsync(user, cancellationToken);
            }
        }
    }
}
