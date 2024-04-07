using HelpDeskMaster.Domain.Authentication;
using HelpDeskMaster.Domain.Authorization;

namespace HelpDeskMaster.Domain.Entities.Users.Intentions
{
    internal class ManageUserIntentionResolver : IIntentionResolver<ManageUserIntention>
    {
        public bool Resolve(IIdentity subject, ManageUserIntention intention)
        {
            if (!subject.IsAuthenticated()) return false;

            return intention switch
            {
                ManageUserIntention.GetUserByLogin => subject.IsAdmin() || subject.IsHelpDeskMember(),
                _ => false
            };
        }
    }
}
