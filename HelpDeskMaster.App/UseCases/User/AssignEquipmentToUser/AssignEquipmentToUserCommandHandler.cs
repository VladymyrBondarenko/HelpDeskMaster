using HelpDeskMaster.Persistence.Data;
using HelpDeskMaster.Domain.Entities.Users;
using MediatR;
using HelpDeskMaster.Persistence.Data.Repositories;

namespace HelpDeskMaster.App.UseCases.User.AssignEquipmentToUser
{
    internal class AssignEquipmentToUserCommandHandler : IRequestHandler<AssignEquipmentToUserCommand>
    {
        private readonly IUserEquipmentService _userEquipmentService;
        private readonly IUnitOfWork _unitOfWork;

        public AssignEquipmentToUserCommandHandler(ApplicationDbContext dbContext,
            IUserEquipmentService userEquipmentService,
            IUnitOfWork unitOfWork)
        {
            _userEquipmentService = userEquipmentService;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(AssignEquipmentToUserCommand request, CancellationToken cancellationToken)
        {
            await _userEquipmentService.AssignEquipmentToUserAsync(
                request.UserId, request.EquipmentId, 
                request.AssignDate, 
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
