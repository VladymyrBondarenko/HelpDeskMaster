using HelpDeskMaster.Domain.Authentication;
using HelpDeskMaster.Domain.Authorization;

namespace HelpDeskMaster.Domain.Entities.WorkCategories.Intentions
{
    internal class ManageWorkCategoryIntentionResolver : IIntentionResolver<ManageWorkCategoryIntention>
    {
        public bool Resolve(IIdentity subject, ManageWorkCategoryIntention intention)
        {
            if (!subject.IsAuthenticated()) return false;

            return intention switch
            {
                ManageWorkCategoryIntention.Create => subject.IsAdmin(),
                ManageWorkCategoryIntention.Delete => subject.IsAdmin(),
                _ => false
            };
        }
    }
}
