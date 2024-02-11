using HelpDeskMaster.Domain.Authorization;
using HelpDeskMaster.Domain.Entities.EquipmentTypes.Intentions;

namespace HelpDeskMaster.Domain.Entities.EquipmentTypes
{
    public class EquipmentTypeService
    {
        private readonly IIntentionManager _intentionManager;
        private readonly IEquipmentTypeRepository _equipmentTypeRepository;

        public EquipmentTypeService(IIntentionManager intentionManager,
            IEquipmentTypeRepository equipmentTypeRepository)
        {
            _intentionManager = intentionManager;
            _equipmentTypeRepository = equipmentTypeRepository;
        }

        public EquipmentType CreateEquipmentType(string title, TypeOfEquipment typeOfEquipment)
        {
            _intentionManager.ThrowIfForbidden(ManageEquipmentTypeIntention.Create);

            var equipmentType = new EquipmentType(Guid.NewGuid(), 
                title, 
                DateTimeOffset.UtcNow, 
                typeOfEquipment);

            _equipmentTypeRepository.Insert(equipmentType);

            return equipmentType;
        }

        public async Task<bool> UpdateEquipmentTypeAsync(EquipmentType equipmentType, CancellationToken cancellationToken)
        {
            _intentionManager.ThrowIfForbidden(ManageEquipmentTypeIntention.Update);

            return await _equipmentTypeRepository.UpdateAsync(equipmentType, cancellationToken);
        }

        public async Task<bool> DeleteEquipmentTypeAsync(Guid equipmentTypeId, CancellationToken cancellationToken)
        {
            _intentionManager.ThrowIfForbidden(ManageEquipmentTypeIntention.Delete);

            return await _equipmentTypeRepository.DeleteAsync(equipmentTypeId, cancellationToken);
        }
    }
}
