using HelpDeskMaster.Domain.Authentication;
using HelpDeskMaster.Domain.Authorization;

namespace HelpDeskMaster.Domain.Entities.Equipments.Intentions
{
    internal class ManageEquipmentIntentionResolver : IIntentionResolver<ManageEquipmentIntention>
    {
        public bool Resolve(IIdentity subject, ManageEquipmentIntention intention)
        {
            return intention switch
            {
                ManageEquipmentIntention.Create => subject.IsAdmin(),
                ManageEquipmentIntention.Update => subject.IsAdmin(),
                ManageEquipmentIntention.Delete => subject.IsAdmin(),
                _ => false
            };
        }
    }
}
