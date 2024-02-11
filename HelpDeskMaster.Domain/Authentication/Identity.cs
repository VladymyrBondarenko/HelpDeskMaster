namespace HelpDeskMaster.Domain.Authentication
{
    public class Identity : IIdentity
    {
        public Identity(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }

        public bool IsAuthenticated()
        {
            throw new NotImplementedException();
        }

        public bool IsAdmin()
        {
            throw new NotImplementedException();
        }

        public bool IsHelpDeskMember()
        {
            throw new NotImplementedException();
        }
    }
}