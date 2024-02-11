using HelpDeskMaster.Domain.Authentication;
using HelpDeskMaster.Domain.Authorization;

namespace HelpDeskMaster.Domain.Entities.Users.Intentions
{
    internal class ManageEquipmentOwnerIntentionResolver : IIntentionResolver<ManageEquipmentOwnerIntention>
    {
        public bool Resolve(IIdentity subject, ManageEquipmentOwnerIntention intention)
        {
            if (!subject.IsAuthenticated()) return false;

            return intention switch
            {
                ManageEquipmentOwnerIntention.Assign => subject.IsAdmin() || subject.IsHelpDeskMember(),
                ManageEquipmentOwnerIntention.Unassign => subject.IsAdmin() || subject.IsHelpDeskMember(),
                _ => false
            };
        }
    }
}