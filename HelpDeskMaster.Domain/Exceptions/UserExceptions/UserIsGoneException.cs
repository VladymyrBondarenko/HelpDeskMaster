namespace HelpDeskMaster.Domain.Exceptions.UserExceptions
{
    public class UserIsGoneException : DomainException
    {
        public UserIsGoneException(string login) 
            : base(DomainErrorCode.Gone, $"User with login {login} was not found")
        {
        }

        public UserIsGoneException(Guid id)
            : base(DomainErrorCode.Gone, $"User with id {id} was not found")
        {
        }
    }
}
