using HelpDeskMaster.Domain.Authorization;
using HelpDeskMaster.Domain.Entities.Equipments.Intentions;
using HelpDeskMaster.Domain.Exceptions.EquipmentExceptions;

namespace HelpDeskMaster.Domain.Entities.Equipments
{
    public class ComputerEquipmentService : IComputerEquipmentService
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IComputerEquipmentRepository _computerEquipmentRepository;
        private readonly IIntentionManager _intentionManager;

        public ComputerEquipmentService(IEquipmentRepository equipmentRepository,
            IComputerEquipmentRepository computerEquipmentRepository,
            IIntentionManager intentionManager)
        {
            _equipmentRepository = equipmentRepository;
            _computerEquipmentRepository = computerEquipmentRepository;
            _intentionManager = intentionManager;
        }

        public async Task AssignEquipmentToComputerAsync(
            Guid computerId, Guid equipmentId, DateTimeOffset assignDate, 
            CancellationToken cancellationToken)
        {
            await _intentionManager.ThrowIfForbiddenAsync(ManageComputerEquipmentIntention.Assign,
                cancellationToken);

            var computer = await _equipmentRepository.GetEquipmentByIdAsync(computerId, cancellationToken);

            if (computer == null)
            {
                throw new EquipmentIsGoneException(computerId);
            }

            if (computer.Id == equipmentId)
            {
                throw new AssignComputerToItselfException(computer.Id);
            }

            if (await _computerEquipmentRepository.IsEquipmentComputerAsync(computer.Id, cancellationToken) == false)
            {
                throw new AssignEquipmentNotToComputerException(computer.Id);
            }

            if (await _computerEquipmentRepository.IsEquipmentComputerAsync(equipmentId, cancellationToken))
            {
                throw new AssignToComputerNotEquipmentException(equipmentId);
            }

            if (await _computerEquipmentRepository.IsEquipmentAssignedAsync(equipmentId, assignDate, cancellationToken))
            {
                throw new EquipmentAlreadyAssignedToComputerException(equipmentId);
            }

            var computerEquipment = computer.AssignEquipmentToComputer(equipmentId, assignDate);

            await _computerEquipmentRepository.InsertAsync(computerEquipment, cancellationToken);
        }

        public async Task UnassignEquipmentFromComputerAsync(
            Guid computerId, Guid equipmentId,DateTimeOffset unassignDate, 
            CancellationToken cancellationToken)
        {
            await _intentionManager.ThrowIfForbiddenAsync(ManageComputerEquipmentIntention.Unassign,
                cancellationToken);

            var computer = await _equipmentRepository.GetEquipmentByIdAsync(computerId, cancellationToken);

            if (computer == null)
            {
                throw new EquipmentIsGoneException(computerId);
            }

            var computerEquipment = computer.ComputerEquipments.SingleOrDefault(x =>
                x.EquipmentId == equipmentId
                && x.AssignedDate <= unassignDate
                && x.UnassignedDate == null);

            if (computerEquipment == null)
            {
                throw new ComputerEquipmentNotFoundToUnassignException(computer.Id, equipmentId);
            }

            computerEquipment.UnassignEquipmentFromComputer(unassignDate);
        }
    }
}