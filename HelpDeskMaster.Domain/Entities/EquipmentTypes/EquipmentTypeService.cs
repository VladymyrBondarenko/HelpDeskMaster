using HelpDeskMaster.Domain.Authorization;
using HelpDeskMaster.Domain.Entities.EquipmentTypes.Intentions;

namespace HelpDeskMaster.Domain.Entities.EquipmentTypes
{
    internal class EquipmentTypeService : IEquipmentTypeService
    {
        private readonly IIntentionManager _intentionManager;
        private readonly IEquipmentTypeRepository _equipmentTypeRepository;

        public EquipmentTypeService(IIntentionManager intentionManager,
            IEquipmentTypeRepository equipmentTypeRepository)
        {
            _intentionManager = intentionManager;
            _equipmentTypeRepository = equipmentTypeRepository;
        }

        public async Task<EquipmentType> CreateEquipmentTypeAsync(string title, TypeOfEquipment typeOfEquipment,
            CancellationToken cancellationToken)
        {
            await _intentionManager.ThrowIfForbiddenAsync(ManageEquipmentTypeIntention.Create,
                cancellationToken);

            var equipmentType = new EquipmentType(Guid.NewGuid(),
                title,
                DateTimeOffset.UtcNow,
                typeOfEquipment);

            await _equipmentTypeRepository.InsertAsync(equipmentType, cancellationToken);

            return equipmentType;
        }

        public async Task UpdateEquipmentTypeAsync(EquipmentType equipmentType, CancellationToken cancellationToken)
        {
            await _intentionManager.ThrowIfForbiddenAsync(ManageEquipmentTypeIntention.Update,
                cancellationToken);

            _equipmentTypeRepository.Update(equipmentType);
        }

        public async Task DeleteEquipmentTypeAsync(Guid equipmentTypeId, CancellationToken cancellationToken)
        {
            await _intentionManager.ThrowIfForbiddenAsync(ManageEquipmentTypeIntention.Delete,
                cancellationToken);

            await _equipmentTypeRepository.DeleteAsync(equipmentTypeId, cancellationToken);
        }
    }
}
