using HelpDeskMaster.Domain.Authorization;
using HelpDeskMaster.Domain.Entities.Users;
using HelpDeskMaster.Domain.Entities.WorkRequests.Intentions;
using HelpDeskMaster.Domain.Entities.WorkRequestStageChanges;
using HelpDeskMaster.Domain.Exceptions.WorkRequestExceptions;

namespace HelpDeskMaster.Domain.Entities.WorkRequests
{
    public class WorkRequestService
    {
        private readonly IIntentionManager _intentionManager;
        private readonly IUserEquipmentRepository _userEquipmentRepository;

        public WorkRequestService(IIntentionManager intentionManager,
            IUserEquipmentRepository userEquipmentRepository)
        {
            _intentionManager = intentionManager;
            _userEquipmentRepository = userEquipmentRepository;
        }

        public async Task AssignExecuterToRequestAsync(WorkRequest workRequest, Guid executerId, 
            CancellationToken cancellationToken)
        {
            await _intentionManager.ThrowIfForbiddenAsync(ManageRequestExecutorIntention.Assign, 
                cancellationToken);

            if (workRequest.RequestStageChanges.Count == 0)
            {
                throw new WorkRequestStageChangesHistoryGoneException(workRequest.Id);
            }

            var lastStage = workRequest.RequestStageChanges
                .OrderByDescending(x => x.CreatedAt).First();

            if(lastStage.Stage != WorkRequestStage.Assignment)
            {
                throw new WorkRequestAssigningExecutorToRequestStageException(executerId, lastStage.Stage);
            }

            workRequest.AssignExecuterToRequest(executerId);
        }

        public async Task UnassignExecuterFromRequestAsync(WorkRequest workRequest, CancellationToken cancellationToken)
        {
            await _intentionManager.ThrowIfForbiddenAsync(ManageRequestExecutorIntention.Unassign, 
                cancellationToken);

            if (workRequest.RequestStageChanges.Count == 0)
            {
                throw new WorkRequestStageChangesHistoryGoneException(workRequest.Id);
            }

            var lastStage = workRequest.RequestStageChanges
                .OrderByDescending(x => x.CreatedAt).First();

            if (lastStage.Stage != WorkRequestStage.Assignment)
            {
                throw new WorkRequestUnassigningExecutorToRequestStageException(workRequest.ExecuterId, lastStage.Stage);
            }

            workRequest.UnassignExecuterFromRequest();
        }

        public async Task<WorkRequestEquipment> AddEquipmentToRequestAsync(WorkRequest workRequest, Guid equipmentId, 
            CancellationToken cancellationToken)
        {
            await _intentionManager.ThrowIfForbiddenAsync(ManageRequestEquipmentIntention.Add, workRequest,
                cancellationToken);

            if (await _userEquipmentRepository.IsEquipmentAssignedToUserAsync(
                equipmentId, workRequest.AuthorId, workRequest.CreatedAt, cancellationToken) == false)
            {
                throw new EquipmentIsNotAssignedToWorkRequestAuthorException(equipmentId, workRequest.AuthorId);
            }

            return workRequest.AddEquipmentToRequest(equipmentId);
        }

        public async Task<bool> RemoveEquipmentFromRequestAsync(WorkRequest workRequest, Guid workRequestEquipmentId,
            CancellationToken cancellationToken)
        {
            await _intentionManager.ThrowIfForbiddenAsync(ManageRequestEquipmentIntention.Remove, workRequest, 
                cancellationToken);

            var workRequestEquipment = workRequest.Equipments.FirstOrDefault(x => x.Id == workRequestEquipmentId);

            if (workRequestEquipment == null)
            {
                throw new WorkRequestEquipmentIsGoneException(workRequest.Id, workRequestEquipmentId);
            }

            if (await _userEquipmentRepository.IsEquipmentAssignedToUserAsync(
                workRequestEquipment.EquipmentId, workRequest.AuthorId, workRequest.CreatedAt, cancellationToken) == false)
            {
                throw new EquipmentIsNotAssignedToWorkRequestAuthorException(
                    workRequestEquipment.EquipmentId, workRequest.AuthorId);
            }

            return workRequest.RemoveEquipmentFromRequest(workRequestEquipment);
        }
    }
}
