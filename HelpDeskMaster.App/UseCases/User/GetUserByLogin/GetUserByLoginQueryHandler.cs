using HelpDeskMaster.Domain.Entities.Users;
using MediatR;

namespace HelpDeskMaster.App.UseCases.User.GetUserByLogin
{
    internal class GetUserByLoginQueryHandler : IRequestHandler<GetUserByLoginQuery, Domain.Entities.Users.User>
    {
        private readonly IUserService _userService;

        public GetUserByLoginQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Domain.Entities.Users.User> Handle(GetUserByLoginQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetUserByLoginAsync(request.Login, cancellationToken);
        }
    }
}
