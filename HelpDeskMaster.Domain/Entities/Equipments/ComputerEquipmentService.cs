using HelpDeskMaster.Domain.Authorization;
using HelpDeskMaster.Domain.Entities.Equipments.Intentions;
using HelpDeskMaster.Domain.Exceptions.EquipmentExceptions;

namespace HelpDeskMaster.Domain.Entities.Equipments
{
    public class ComputerEquipmentService
    {
        private readonly IComputerEquipmentRepository _computerEquipmentRepository;
        private readonly IIntentionManager _intentionManager;

        public ComputerEquipmentService(IComputerEquipmentRepository computerEquipmentRepository,
            IIntentionManager intentionManager)
        {
            _computerEquipmentRepository = computerEquipmentRepository;
            _intentionManager = intentionManager;
        }

        public async Task AssignEquipmentToComputerAsync(Equipment computer,
            Guid equipmentId, DateTimeOffset assignDate, CancellationToken cancellationToken)
        {
            _intentionManager.ThrowIfForbidden(ManageComputerEquipmentIntention.Assign);

            if (computer.Id == equipmentId)
            {
                throw new AssignComputerToItselfException(computer.Id);
            }

            if(await _computerEquipmentRepository.IsEquipmentComputerAsync(computer.Id, cancellationToken) == false)
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

            _computerEquipmentRepository.Insert(computerEquipment);
        }

        public void UnassignEquipmentFromComputer(Equipment computer, Guid equipmentId,
            DateTimeOffset unassignDate)
        {
            _intentionManager.ThrowIfForbidden(ManageComputerEquipmentIntention.Unassign);

            var computerEquipment = computer.ComputerEquipments.SingleOrDefault(x =>
                x.EquipmentId == equipmentId
                && x.AssignedDate <= unassignDate
                && x.UnassignedDate == null);

            if(computerEquipment == null)
            {
                throw new ComputerEquipmentNotFoundToUnassignException(computer.Id, equipmentId);
            }

            computerEquipment.UnassignEquipmentFromComputer(unassignDate);
        }
    }
}