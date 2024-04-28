using HelpDeskMaster.Domain.Authorization;
using HelpDeskMaster.Domain.Entities.Users.Intentions;
using HelpDeskMaster.Domain.Exceptions.UserExceptions;

namespace HelpDeskMaster.Domain.Entities.Users
{
    internal class UserEquipmentService : IUserEquipmentService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserEquipmentRepository _userEquipmentRepository;
        private readonly IIntentionManager _intentionManager;

        public UserEquipmentService(
            IUserRepository userRepository,
            IUserEquipmentRepository userEquipmentRepository,
            IIntentionManager intentionManager)
        {
            _userRepository = userRepository;
            _userEquipmentRepository = userEquipmentRepository;
            _intentionManager = intentionManager;
        }

        public async Task AssignEquipmentToUserAsync(
            Guid userId, Guid equipmentId, DateTimeOffset assignDate,
            CancellationToken cancellationToken)
        {
            await _intentionManager.ThrowIfForbiddenAsync(ManageEquipmentOwnerIntention.Assign,
                cancellationToken);

            var user = await _userRepository.GetUserByIdAsync(userId, cancellationToken);

            if (user == null)
            {
                throw new UserIsGoneException(userId);
            }

            if (await _userEquipmentRepository.IsEquipmentAssignedAsync(equipmentId, assignDate, cancellationToken))
            {
                throw new EquipmentAlreadyAssignedToUserException(equipmentId);
            }

            var userEquipment = user.AssignEquipment(equipmentId, assignDate);

            await _userEquipmentRepository.InsertAsync(userEquipment, cancellationToken);
        }

        public async Task UnassignEquipmentFromUserAsync(
            Guid userId, Guid equipmentId, DateTimeOffset unassignDate,
            CancellationToken cancellationToken)
        {
            await _intentionManager.ThrowIfForbiddenAsync(ManageEquipmentOwnerIntention.Unassign,
                cancellationToken);

            var user = await _userRepository.GetUserByIdAsync(userId, cancellationToken);

            if (user == null)
            {
                throw new UserIsGoneException(userId);
            }

            var userEquipment = user.Equipments.SingleOrDefault(x =>
                x.EquipmentId == equipmentId
                && x.AssignedDate <= unassignDate
                && x.UnassignedDate == null);

            if (userEquipment == null)
            {
                throw new UserEquipmentNotFoundToUnassignException(user.Id, equipmentId);
            }

            userEquipment.UnassignEquipment(unassignDate);
        }
    }
}