using HelpDeskMaster.Domain.Authorization;
using HelpDeskMaster.Domain.Entities.Equipments.Intentions;

namespace HelpDeskMaster.Domain.Entities.Equipments
{
    internal class EquipmentService : IEquipmentService
    {
        private readonly IIntentionManager _intentionManager;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IEquipmentComputerInfoRepository _computerInfoRepository;

        public EquipmentService(IIntentionManager intentionManager,
            IEquipmentRepository equipmentRepository,
            IEquipmentComputerInfoRepository computerInfoRepository)
        {
            _intentionManager = intentionManager;
            _equipmentRepository = equipmentRepository;
            _computerInfoRepository = computerInfoRepository;
        }

        public async Task<Equipment> CreateEquipmentAsync(
            Guid equipmentTypeId,
            string? model,
            DateTimeOffset commissioningDate,
            string? factoryNumber,
            decimal price,
            CancellationToken cancellationToken)
        {
            await _intentionManager.ThrowIfForbiddenAsync(ManageEquipmentIntention.Create,
                cancellationToken);

            var equipment = new Equipment(Guid.NewGuid(),
                equipmentTypeId,
                model,
                commissioningDate,
                factoryNumber,
                price,
                DateTimeOffset.UtcNow);

            await _equipmentRepository.InsertAsync(equipment, cancellationToken);

            return equipment;
        }

        public async Task<Equipment> CreateComputerAsync(
            Guid equipmentTypeId,
            string? model,
            DateTimeOffset commissioningDate,
            string? factoryNumber,
            decimal price,
            string code,
            string nameInNet,
            int warrantyMonths,
            DateTimeOffset invoiceDate,
            DateTimeOffset warrantyCardDate,
            CancellationToken cancellationToken)
        {
            await _intentionManager.ThrowIfForbiddenAsync(ManageEquipmentIntention.Create,
                cancellationToken);

            var computerId = Guid.NewGuid();

            var computerInfo = new EquipmentComputerInfo(
                Guid.NewGuid(),
                computerId,
                code,
                nameInNet,
                warrantyMonths,
                invoiceDate,
                warrantyCardDate,
                DateTimeOffset.UtcNow);
            await _computerInfoRepository.InsertAsync(computerInfo, cancellationToken);

            var equipment = new Equipment(
                computerId,
                equipmentTypeId,
                model,
                commissioningDate,
                factoryNumber,
                price,
                DateTimeOffset.UtcNow);

            equipment.AddComputerInfo(computerInfo);

            await _equipmentRepository.InsertAsync(equipment, cancellationToken);

            return equipment;
        }

        public async Task UpdateEquipmentAsync(Equipment equipment, CancellationToken cancellationToken)
        {
            await _intentionManager.ThrowIfForbiddenAsync(ManageEquipmentIntention.Update,
                cancellationToken);

            _equipmentRepository.Update(equipment);
        }

        public async Task DeleteEquipmentAsync(Guid equipmentId, CancellationToken cancellationToken)
        {
            await _intentionManager.ThrowIfForbiddenAsync(ManageEquipmentIntention.Delete,
                cancellationToken);

            await _equipmentRepository.DeleteAsync(equipmentId, cancellationToken);
        }
    }
}