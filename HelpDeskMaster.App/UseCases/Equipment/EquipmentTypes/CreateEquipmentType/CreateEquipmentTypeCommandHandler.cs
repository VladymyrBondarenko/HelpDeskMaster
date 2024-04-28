using HelpDeskMaster.Domain.Entities.EquipmentTypes;
using HelpDeskMaster.Persistence.Data.Repositories;
using MediatR;

namespace HelpDeskMaster.App.UseCases.Equipment.EquipmentTypes.CreateEquipmentType
{
    internal class CreateEquipmentTypeCommandHandler : IRequestHandler<CreateEquipmentTypeCommand, EquipmentType>
    {
        private readonly IEquipmentTypeService _equipmentTypeService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEquipmentTypeCommandHandler(IEquipmentTypeService equipmentTypeService,
            IUnitOfWork unitOfWork)
        {
            _equipmentTypeService = equipmentTypeService;
            _unitOfWork = unitOfWork;
        }

        public async Task<EquipmentType> Handle(CreateEquipmentTypeCommand request, CancellationToken cancellationToken)
        {
            var equipmentType = await _equipmentTypeService.CreateEquipmentTypeAsync(
                request.Title, request.TypeOfEquipment, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return equipmentType;
        }
    }
}
