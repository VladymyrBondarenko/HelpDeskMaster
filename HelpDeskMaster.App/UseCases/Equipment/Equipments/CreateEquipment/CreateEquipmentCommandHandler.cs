using HelpDeskMaster.Domain.Entities.Equipments;
using HelpDeskMaster.Persistence.Data.Repositories;
using MediatR;

namespace HelpDeskMaster.App.UseCases.Equipment.Equipments.CreateEquipment
{
    internal class CreateEquipmentCommandHandler : IRequestHandler<CreateEquipmentCommand, Domain.Entities.Equipments.Equipment>
    {
        private readonly IEquipmentService _equipmentService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEquipmentCommandHandler(IEquipmentService equipmentService,
            IUnitOfWork unitOfWork)
        {
            _equipmentService = equipmentService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Domain.Entities.Equipments.Equipment> Handle(CreateEquipmentCommand request, CancellationToken cancellationToken)
        {
            var equipment = await _equipmentService.CreateEquipmentAsync(
                request.EquipmentTypeId,
                request.Model,
                request.CommissioningDate,
                request.FactoryNumber,
                request.Price,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return equipment;
        }
    }
}
