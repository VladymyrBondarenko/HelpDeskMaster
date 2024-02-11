using HelpDeskMaster.Domain.Authentication;
using HelpDeskMaster.Domain.Authorization;

namespace HelpDeskMaster.Domain.Entities.WorkRequests.Intentions
{
    internal class ManageRequestExecutorIntentionResolver : IIntentionResolver<ManageRequestExecutorIntention>
    {
        public bool Resolve(IIdentity subject, ManageRequestExecutorIntention intention)
        {
            if (!subject.IsAuthenticated()) return false;

            return intention switch
            {
                ManageRequestExecutorIntention.Assign => subject.IsAdmin() || subject.IsHelpDeskMember(),
                ManageRequestExecutorIntention.Unassign => subject.IsAdmin() || subject.IsHelpDeskMember(),
                _ => false,
            };
        }
    }
}