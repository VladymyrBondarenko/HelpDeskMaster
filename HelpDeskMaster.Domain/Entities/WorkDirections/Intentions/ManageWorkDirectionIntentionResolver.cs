using HelpDeskMaster.Domain.Authentication;
using HelpDeskMaster.Domain.Authorization;

namespace HelpDeskMaster.Domain.Entities.WorkDirections.Intentions
{
    internal class ManageWorkDirectionIntentionResolver : IIntentionResolver<ManageWorkDirectionIntention>
    {
        public bool Resolve(IIdentity subject, ManageWorkDirectionIntention intention)
        {
            if (!subject.IsAuthenticated()) return false;

            return intention switch
            {
                ManageWorkDirectionIntention.Create => subject.IsAdmin(),
                ManageWorkDirectionIntention.Delete => subject.IsAdmin(),
                _ => false
            };
        }
    }
}
