using HelpDeskMaster.Domain.Authorization;
using HelpDeskMaster.Domain.Entities.Equipments.Intentions;

namespace HelpDeskMaster.Domain.Entities.Equipments
{
    public class EquipmentService
    {
        private readonly IIntentionManager _intentionManager;
        private readonly IEquipmentRepository _equipmentRepository;

        public EquipmentService(IIntentionManager intentionManager,
            IEquipmentRepository equipmentRepository)
        {
            _intentionManager = intentionManager;
            _equipmentRepository = equipmentRepository;
        }

        public async Task<Equipment> CreateEquipmentAsync(Guid equipmentTypeId,
            string? model,
            DateTimeOffset commissioningDate,
            string? factoryNumber,
            decimal price,
            Guid? departmentId,
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
                departmentId,
                DateTimeOffset.UtcNow);

            _equipmentRepository.Insert(equipment);

            return equipment;
        }

        public async Task<bool> UpdateEquipmentAsync(Equipment equipment, CancellationToken cancellationToken)
        {
            await _intentionManager.ThrowIfForbiddenAsync(ManageEquipmentIntention.Update, 
                cancellationToken);

            return await _equipmentRepository.UpdateAsync(equipment, cancellationToken);
        }

        public async Task<bool> DeleteEquipmentAsync(Guid equipmentId, CancellationToken cancellationToken)
        {
            await _intentionManager.ThrowIfForbiddenAsync(ManageEquipmentIntention.Delete, 
                cancellationToken);

            return await _equipmentRepository.DeleteAsync(equipmentId, cancellationToken);
        }
    }
}