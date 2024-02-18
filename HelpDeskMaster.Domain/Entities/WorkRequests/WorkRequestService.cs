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

        public void AssignExecuterToRequest(WorkRequest workRequest, Guid executerId)
        {
            _intentionManager.ThrowIfForbidden(ManageRequestExecutorIntention.Assign);

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

        public void UnassignExecuterFromRequest(WorkRequest workRequest)
        {
            _intentionManager.ThrowIfForbidden(ManageRequestExecutorIntention.Unassign);

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
            _intentionManager.ThrowIfForbidden(ManageRequestEquipmentIntention.Add, workRequest);

            if (await _userEquipmentRepository.IsEquipmentAssignedToUserAsync(
                equipmentId, workRequest.AuthorId, cancellationToken) == false)
            {
                throw new EquipmentIsNotAssignedToWorkRequestAuthorException(equipmentId, workRequest.AuthorId);
            }

            return workRequest.AddEquipmentToRequest(equipmentId);
        }

        public async Task<bool> RemoveEquipmentFromRequestAsync(WorkRequest workRequest, Guid workRequestEquipmentId,
            CancellationToken cancellationToken)
        {
            _intentionManager.ThrowIfForbidden(ManageRequestEquipmentIntention.Remove, workRequest);

            var workRequestEquipment = workRequest.Equipments.FirstOrDefault(x => x.Id == workRequestEquipmentId);

            if (workRequestEquipment == null)
            {
                throw new WorkRequestEquipmentIsGoneException(workRequest.Id, workRequestEquipmentId);
            }

            if (await _userEquipmentRepository.IsEquipmentAssignedToUserAsync(
                workRequestEquipment.EquipmentId, workRequest.AuthorId, cancellationToken) == false)
            {
                throw new EquipmentIsNotAssignedToWorkRequestAuthorException(
                    workRequestEquipment.EquipmentId, workRequest.AuthorId);
            }

            return workRequest.RemoveEquipmentFromRequest(workRequestEquipment);
        }
    }
}
