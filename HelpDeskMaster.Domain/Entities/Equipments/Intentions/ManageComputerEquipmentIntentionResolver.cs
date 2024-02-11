using HelpDeskMaster.Domain.Authentication;
using HelpDeskMaster.Domain.Authorization;

namespace HelpDeskMaster.Domain.Entities.Equipments.Intentions
{
    internal class ManageComputerEquipmentIntentionResolver : IIntentionResolver<ManageComputerEquipmentIntention>
    {
        public bool Resolve(IIdentity subject, ManageComputerEquipmentIntention intention)
        {
            if (!subject.IsAuthenticated()) return false;

            return intention switch
            {
                ManageComputerEquipmentIntention.Assign => subject.IsAdmin() || subject.IsHelpDeskMember(),
                ManageComputerEquipmentIntention.Unassign => subject.IsAdmin() || subject.IsHelpDeskMember(),
                _ => false
            };
        }
    }
}