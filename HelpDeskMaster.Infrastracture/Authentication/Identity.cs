using HelpDeskMaster.Domain.Authentication;

namespace HelpDeskMaster.Infrastracture.Authentication
{
    internal class Identity : IIdentity
    {
        private readonly HashSet<string> _roles;

        public Identity(Guid userId, HashSet<string> roles)
        {
            UserId = userId;
            _roles = roles;
        }

        public Guid UserId { get; }

        public bool IsAdmin()
        {
            return _roles.Contains(RolesConstants.Admin);
        }

        public bool IsHelpDeskMember()
        {
            return _roles.Contains(RolesConstants.HelpDeskMember);
        }

        public bool IsAuthenticated()
        {
            return true;
        }
    }
}
