using Ardalis.GuardClauses;
using HelpDeskMaster.Domain.Abstractions;
using HelpDeskMaster.Domain.Entities.WorkRequestStageChanges;

namespace HelpDeskMaster.Domain.Entities.WorkRequests
{
    public class WorkRequest : AggregateRoot
    {
        private WorkRequest(Guid id, DateTimeOffset createdAt,
            Guid authorId,
            Guid directionId,
            Guid categoryId,
            DateTimeOffset failureRevealedDate,
            DateTimeOffset desiredExecutionDate,
            string? content) : base(id, createdAt)
        {
            AuthorId = Guard.Against.Default(authorId);
            DirectionId = Guard.Against.Default(directionId);
            CategoryId = Guard.Against.Default(categoryId);
            FailureRevealedDate = Guard.Against.Default(failureRevealedDate);
            DesiredExecutionDate = Guard.Against.Default(desiredExecutionDate);
            Content = content;
        }

        public Guid AuthorId { get; private set; }

        public Guid? ExecuterId { get; private set; }

        public int RequestNumber { get; }

        public Guid DirectionId { get; private set; }

        public Guid CategoryId { get; private set; }

        public DateTimeOffset FailureRevealedDate { get; private set; }

        public DateTimeOffset DesiredExecutionDate { get; private set; }

        public string? Content { get; private set; }

        private HashSet<WorkRequestPost> _posts = new();

        public IReadOnlyList<WorkRequestStageChange> RequestStageChanges => _requestStageChanges.ToList();

        private HashSet<WorkRequestStageChange> _requestStageChanges = new();

        public IReadOnlyList<WorkRequestPost> WorkRequestPosts => _posts.ToList();

        public IReadOnlyList<WorkRequestEquipment> Equipments => _equipments.ToList();

        private HashSet<WorkRequestEquipment> _equipments = new();

        public static WorkRequest Create(Guid id, DateTimeOffset createdAt,
            Guid ownerId,
            Guid directionId,
            Guid categoryId,
            DateTimeOffset failureRevealedDate,
            DateTimeOffset desiredExecutionDate,
            string? content)
        {
            var workRequest = new WorkRequest(id, 
                createdAt,
                ownerId,
                directionId, 
                categoryId, 
                failureRevealedDate, 
                desiredExecutionDate, 
                content);

            workRequest.ChangeRequestStage(WorkRequestStage.NewRequest);

            return workRequest;
        }

        public WorkRequestPost AddPostToRequest(Guid userId, string content)
        {
            var workRequestPost = new WorkRequestPost(Guid.NewGuid(), DateTimeOffset.UtcNow,
                userId, Id, content);

            _posts.Add(workRequestPost);

            return workRequestPost;
        }

        internal WorkRequestEquipment AddEquipmentToRequest(Guid equipmentId)
        {
            var workRequestEquipment = new WorkRequestEquipment(Guid.NewGuid(),
                DateTimeOffset.UtcNow, Id, equipmentId);

            _equipments.Add(workRequestEquipment);

            return workRequestEquipment;
        }

        internal bool RemoveEquipmentFromRequest(WorkRequestEquipment requestEquipment)
        {
            return _equipments.Remove(requestEquipment);
        }

        internal void AssignExecuterToRequest(Guid executerId)
        {
            ExecuterId = Guard.Against.Default(executerId);
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        internal void UnassignExecuterFromRequest()
        {
            ExecuterId = null;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        internal WorkRequestStageChange ChangeRequestStage(WorkRequestStage stage)
        {
            var requestStageChange = new WorkRequestStageChange(Guid.NewGuid(),
                DateTimeOffset.UtcNow, Id, stage);

            _requestStageChanges.Add(requestStageChange);

            return requestStageChange;
        }
    }
}