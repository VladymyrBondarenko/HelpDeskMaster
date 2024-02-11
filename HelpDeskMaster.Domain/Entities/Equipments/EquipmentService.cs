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

        public Equipment CreateEquipment(Guid equipmentTypeId,
            string? model,
            DateTimeOffset commissioningDate,
            string? factoryNumber,
            decimal price,
            Guid? departmentId)
        {
            _intentionManager.ThrowIfForbidden(ManageEquipmentIntention.Create);

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

        public async Task<bool> UpdateEquipment(Equipment equipment, CancellationToken cancellationToken)
        {
            _intentionManager.ThrowIfForbidden(ManageEquipmentIntention.Update);

            return await _equipmentRepository.UpdateAsync(equipment, cancellationToken);
        }

        public async Task<bool> DeleteEquipment(Guid equipmentId, CancellationToken cancellationToken)
        {
            _intentionManager.ThrowIfForbidden(ManageEquipmentIntention.Delete);

            return await _equipmentRepository.DeleteAsync(equipmentId, cancellationToken);
        }
    }
}