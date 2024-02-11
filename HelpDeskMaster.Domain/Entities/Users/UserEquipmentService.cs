using HelpDeskMaster.Domain.Authorization;
using HelpDeskMaster.Domain.Entities.Users.Intentions;
using HelpDeskMaster.Domain.Exceptions.UserExceptions;

namespace HelpDeskMaster.Domain.Entities.Users
{
    public class UserEquipmentService
    {
        private readonly IUserEquipmentRepository _userEquipmentRepository;
        private readonly IIntentionManager _intentionManager;

        public UserEquipmentService(IUserEquipmentRepository userEquipmentRepository,
            IIntentionManager intentionManager)
        {
            _userEquipmentRepository = userEquipmentRepository;
            _intentionManager = intentionManager;
        }

        public async Task AssignEquipmentToUserAsync(User user, Guid equipmentId, DateTimeOffset assignDate, 
            CancellationToken cancellationToken)
        {
            _intentionManager.ThrowIfForbidden(ManageEquipmentOwnerIntention.Assign);

            if (await _userEquipmentRepository.IsEquipmentAssignedAsync(equipmentId, assignDate, cancellationToken))
            {
                throw new EquipmentAlreadyAssignedToUserException(equipmentId);
            }

            var userEquipment = user.AssignEquipment(equipmentId, assignDate);

            _userEquipmentRepository.Insert(userEquipment);
        }

        public void UnassignEquipmentFromUser(User user, Guid equipmentId, DateTimeOffset unassignDate)
        {
            _intentionManager.ThrowIfForbidden(ManageEquipmentOwnerIntention.Unassign);

            var userEquipment = user.Equipments.SingleOrDefault(x =>
                x.EquipmentId == equipmentId
                && x.AssignedDate <= unassignDate
                && x.UnassignedDate == null);

            if(userEquipment == null)
            {
                throw new UserEquipmentNotFoundToUnassignException(user.Id, equipmentId);
            }

            userEquipment.UnassignEquipment(unassignDate);
        }
    }
}