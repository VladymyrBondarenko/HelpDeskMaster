using HelpDeskMaster.Domain.Entities.Equipments;
using HelpDeskMaster.Persistence.Data.Repositories;
using MediatR;

namespace HelpDeskMaster.App.UseCases.Equipment.Equipments.AssignEquipmentToComputer
{
    internal class AssignEquipmentToComputerCommandHandler : IRequestHandler<AssignEquipmentToComputerCommand>
    {
        private readonly IComputerEquipmentService _computerEquipmentService;
        private readonly IUnitOfWork _unitOfWork;

        public AssignEquipmentToComputerCommandHandler(
            IComputerEquipmentService computerEquipmentService,
            IUnitOfWork unitOfWork)
        {
            _computerEquipmentService = computerEquipmentService;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(AssignEquipmentToComputerCommand request, CancellationToken cancellationToken)
        {
            await _computerEquipmentService.AssignEquipmentToComputerAsync(
                request.ComputerId,
                request.EquipmentId,
                request.AssignDate,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
