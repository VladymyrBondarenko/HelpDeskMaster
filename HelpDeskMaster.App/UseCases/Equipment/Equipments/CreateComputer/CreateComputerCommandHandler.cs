using HelpDeskMaster.Domain.Entities.Equipments;
using HelpDeskMaster.Persistence.Data.Repositories;
using MediatR;

namespace HelpDeskMaster.App.UseCases.Equipment.Equipments.CreateComputer
{
    internal class CreateComputerCommandHandler : IRequestHandler<CreateComputerCommand, Domain.Entities.Equipments.Equipment>
    {
        private readonly IEquipmentService _equipmentService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateComputerCommandHandler(IEquipmentService equipmentService,
            IUnitOfWork unitOfWork)
        {
            _equipmentService = equipmentService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Domain.Entities.Equipments.Equipment> Handle(
            CreateComputerCommand request, CancellationToken cancellationToken)
        {
            var computer = await _equipmentService.CreateComputerAsync(
                request.EquipmentTypeId,
                request.Model,
                request.CommissioningDate,
                request.FactoryNumber,
                request.Price,
                request.Code,
                request.NameInNet, 
                request.WarrantyMonths, 
                request.InvoiceDate, 
                request.WarrantyCardDate, 
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return computer;
        }
    }
}
