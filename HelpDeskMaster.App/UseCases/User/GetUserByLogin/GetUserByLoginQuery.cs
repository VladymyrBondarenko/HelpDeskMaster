using MediatR;

namespace HelpDeskMaster.App.UseCases.User.GetUserByLogin
{
    public record class GetUserByLoginQuery(string Login) : IRequest<Domain.Entities.Users.User>
    {
    }
}
