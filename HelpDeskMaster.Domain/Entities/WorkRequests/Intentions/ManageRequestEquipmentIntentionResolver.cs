using HelpDeskMaster.Domain.Authentication;
using HelpDeskMaster.Domain.Authorization;

namespace HelpDeskMaster.Domain.Entities.WorkRequests.Intentions
{
    internal class ManageRequestEquipmentIntentionResolver : IIntentionResolver<ManageRequestEquipmentIntention, WorkRequest>
    {
        public bool Resolve(IIdentity subject, WorkRequest workRequest, ManageRequestEquipmentIntention intention)
        {
            return intention switch
            {
                ManageRequestEquipmentIntention.Add => subject.UserId == workRequest.AuthorId,
                ManageRequestEquipmentIntention.Remove => subject.UserId == workRequest.AuthorId,
                _ => false
            };
        }
    }
}