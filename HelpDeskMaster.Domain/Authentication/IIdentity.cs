namespace HelpDeskMaster.Domain.Authentication
{
    public interface IIdentity
    {
        public Guid UserId { get; }

        bool IsAuthenticated();

        bool IsAdmin();

        bool IsHelpDeskMember();
    }
}