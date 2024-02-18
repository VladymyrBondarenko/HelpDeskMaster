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
            return true;
        }

        public bool IsAdmin()
        {
            return true;
        }

        public bool IsHelpDeskMember()
        {
            return true;
        }
    }
}