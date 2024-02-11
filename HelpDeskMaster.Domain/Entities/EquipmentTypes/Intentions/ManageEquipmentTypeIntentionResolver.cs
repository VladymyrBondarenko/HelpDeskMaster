using HelpDeskMaster.Domain.Authentication;
using HelpDeskMaster.Domain.Authorization;

namespace HelpDeskMaster.Domain.Entities.EquipmentTypes.Intentions
{
    internal class ManageEquipmentTypeIntentionResolver : IIntentionResolver<ManageEquipmentTypeIntention>
    {
        public bool Resolve(IIdentity subject, ManageEquipmentTypeIntention intention)
        {
            return intention switch
            {
                ManageEquipmentTypeIntention.Create => subject.IsAdmin(),
                ManageEquipmentTypeIntention.Update => subject.IsAdmin(),
                ManageEquipmentTypeIntention.Delete => subject.IsAdmin(),
                _ => false
            };
        }
    }
}
