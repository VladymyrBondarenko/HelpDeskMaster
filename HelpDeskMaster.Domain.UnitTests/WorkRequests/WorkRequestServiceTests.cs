using FluentAssertions;
using HelpDeskMaster.Domain.Authorization;
using HelpDeskMaster.Domain.Entities.Users;
using HelpDeskMaster.Domain.Entities.WorkRequests;
using HelpDeskMaster.Domain.Entities.WorkRequests.Intentions;
using HelpDeskMaster.Domain.Exceptions.WorkRequestExceptions;
using Moq;

namespace HelpDeskMaster.Domain.UnitTests.WorkRequests
{
    public class WorkRequestServiceTests
    {
        private readonly Mock<IIntentionManager> _intentionManagerMock;
        private readonly Mock<IUserEquipmentRepository> _userEquipmentRepositoryMock;
        private WorkRequestService _sut;

        public WorkRequestServiceTests()
        {
            _intentionManagerMock = new Mock<IIntentionManager>();
            _userEquipmentRepositoryMock = new Mock<IUserEquipmentRepository>();

            _sut = new WorkRequestService(_intentionManagerMock.Object, _userEquipmentRepositoryMock.Object);
        }

        #region AssignExecuterToRequest

        [Fact]
        public void AssignExecuterToRequest_ShouldAssignExecuterToRequest()
        {
            var intention = ManageRequestExecutorIntention.Assign;

            _intentionManagerMock.Setup(x => x.IsAllowed(intention))
                .Returns(true);

            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var ownerId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                ownerId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);

            workRequest.ChangeRequestStage(WorkRequestStage.Assignment);

            var executerId = new Guid("350f6ee0-1914-42ad-824f-3fc0872c160d");

            _sut.AssignExecuterToRequest(workRequest, executerId);

            workRequest.ExecuterId.Should().Be(executerId);
        }

        [Fact]
        public void AssignExecuterToRequest_ShouldThrowIntentionManagerException_WhenForbidden()
        {
            var intention = ManageRequestExecutorIntention.Assign;

            _intentionManagerMock.Setup(x => x.IsAllowed(intention))
                .Returns(false);

            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var ownerId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                ownerId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);

            var executerId = new Guid("350f6ee0-1914-42ad-824f-3fc0872c160d");

            _sut.Invoking(x => x.AssignExecuterToRequest(workRequest, executerId))
                .Should().Throw<IntentionManagerException>();
        }

        [Fact]
        public void AssignExecuterToRequest_ShouldThrowWorkRequestAssigningExecutorToRequestStageException_WhenAssignExecuterNotOnAssignmentStage()
        {
            var intention = ManageRequestExecutorIntention.Assign;

            _intentionManagerMock.Setup(x => x.IsAllowed(intention))
                .Returns(true);

            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var ownerId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                ownerId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);

            var executerId = new Guid("350f6ee0-1914-42ad-824f-3fc0872c160d");

            _sut.Invoking(x => x.AssignExecuterToRequest(workRequest, executerId))
                .Should().Throw<WorkRequestAssigningExecutorToRequestStageException>();
        }

        #endregion

        #region UnassignExecuterToRequest

        [Fact]
        public void UnassignExecuterToRequest_ShouldAssignExecuterToRequest()
        {
            var intention = ManageRequestExecutorIntention.Unassign;

            _intentionManagerMock.Setup(x => x.IsAllowed(intention))
                .Returns(true);

            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var ownerId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                ownerId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);

            workRequest.ChangeRequestStage(WorkRequestStage.Assignment);

            _sut.UnassignExecuterFromRequest(workRequest);

            workRequest.ExecuterId.Should().BeNull();
        }

        [Fact]
        public void UnassignExecuterToRequest_ShouldThrowIntentionManagerException_WhenForbidden()
        {
            var intention = ManageRequestExecutorIntention.Unassign;

            _intentionManagerMock.Setup(x => x.IsAllowed(intention))
                .Returns(false);

            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var ownerId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                ownerId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);

            _sut.Invoking(x => x.UnassignExecuterFromRequest(workRequest))
                .Should().Throw<IntentionManagerException>();
        }

        [Fact]
        public void UnassignExecuterToRequest_ShouldThrowWorkRequestAssigningExecutorToRequestStageException_WhenAssignExecuterNotOnAssignmentStage()
        {
            var intention = ManageRequestExecutorIntention.Unassign;

            _intentionManagerMock.Setup(x => x.IsAllowed(intention))
                .Returns(true);

            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var ownerId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                ownerId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);

            var executerId = new Guid("350f6ee0-1914-42ad-824f-3fc0872c160d");

            _sut.Invoking(x => x.UnassignExecuterFromRequest(workRequest))
                .Should().Throw<WorkRequestUnassigningExecutorToRequestStageException>();
        }

        #endregion

        #region AddEquipmentToRequestAsync

        [Fact]
        public async Task AddEquipmentToRequestAsync_ShouldThrowIntentionManagerException_WhenForbidden()
        {
            var equipmentId = new Guid("2f4d29b7-2b87-4989-9316-90cd9f8bf6d6");
            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var authorId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                authorId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);

            _intentionManagerMock
                .Setup(x => x.IsAllowed(ManageRequestEquipmentIntention.Add, workRequest))
                .Returns(false);
            _userEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentAssignedToUserAsync(equipmentId, authorId, CancellationToken.None))
                .ReturnsAsync(true);

            await _sut.Invoking(x => x.AddEquipmentToRequestAsync(workRequest, equipmentId, CancellationToken.None))
                .Should().ThrowAsync<IntentionManagerException>();
        }

        [Fact]
        public async Task AddEquipmentToRequestAsync_ShouldThrowEquipmentIsNotAssignedToWorkRequestAuthorException_WhenEquipmentIsNotAssignedToUser()
        {
            var equipmentId = new Guid("2f4d29b7-2b87-4989-9316-90cd9f8bf6d6");
            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var authorId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                authorId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);

            _intentionManagerMock
                .Setup(x => x.IsAllowed(ManageRequestEquipmentIntention.Add, workRequest))
                .Returns(true);
            _userEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentAssignedToUserAsync(equipmentId, authorId, CancellationToken.None))
                .ReturnsAsync(false);

            await _sut.Invoking(x => x.AddEquipmentToRequestAsync(workRequest, equipmentId, CancellationToken.None))
                .Should().ThrowAsync<EquipmentIsNotAssignedToWorkRequestAuthorException>();
        }

        [Fact]
        public async Task AddEquipmentToRequestAsync_ShouldAddEquipmentToRequest()
        {
            var equipmentId = new Guid("2f4d29b7-2b87-4989-9316-90cd9f8bf6d6");
            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var authorId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                authorId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);

            _intentionManagerMock
                .Setup(x => x.IsAllowed(ManageRequestEquipmentIntention.Add, workRequest))
                .Returns(true);
            _userEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentAssignedToUserAsync(equipmentId, authorId, CancellationToken.None))
                .ReturnsAsync(true);

            var addedEquipment = await _sut.AddEquipmentToRequestAsync(workRequest, equipmentId, CancellationToken.None);

            addedEquipment.Should().NotBeNull();
            workRequest.Equipments.Any(x => x.Id == addedEquipment.Id)
                .Should().BeTrue();
        }

        #endregion

        #region RemoveEquipmentFromRequestAsync

        [Fact]
        public async Task RemoveEquipmentFromRequestAsync_ShouldThrowIntentionManagerException_WhenForbidden()
        {
            var equipmentId = new Guid("2f4d29b7-2b87-4989-9316-90cd9f8bf6d6");
            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var authorId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                authorId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);
            var workRequestEquipment = workRequest.AddEquipmentToRequest(equipmentId);

            _intentionManagerMock
                .Setup(x => x.IsAllowed(ManageRequestEquipmentIntention.Remove, workRequest))
                .Returns(false);

            await _sut.Invoking(x => x.RemoveEquipmentFromRequestAsync(workRequest, workRequestEquipment.Id, CancellationToken.None))
                .Should().ThrowAsync<IntentionManagerException>();
        }

        [Fact]
        public async Task RemoveEquipmentFromRequestAsync_ShouldThrowWorkRequestEquipmentIsGoneException_WhenWorkRequestEquipmentIsGone()
        {
            var equipmentId = new Guid("2f4d29b7-2b87-4989-9316-90cd9f8bf6d6");
            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var authorId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                authorId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);
            var workRequestEquipment = workRequest.AddEquipmentToRequest(equipmentId);

            _intentionManagerMock
                .Setup(x => x.IsAllowed(ManageRequestEquipmentIntention.Remove, workRequest))
                .Returns(true);

            await _sut.Invoking(x => x.RemoveEquipmentFromRequestAsync(workRequest,
                    new Guid("3926ed81-bc51-4a25-b710-1a32b72a8f60"), CancellationToken.None))
                .Should().ThrowAsync<WorkRequestEquipmentIsGoneException>();
        }

        [Fact]
        public async Task RemoveEquipmentFromRequestAsync_ShouldThrowEquipmentIsNotAssignedToWorkRequestAuthorException_WhenEquipmentIsNotAssignedToUser()
        {
            var equipmentId = new Guid("2f4d29b7-2b87-4989-9316-90cd9f8bf6d6");
            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var authorId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                authorId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);
            var workRequestEquipment = workRequest.AddEquipmentToRequest(equipmentId);

            _intentionManagerMock
                .Setup(x => x.IsAllowed(ManageRequestEquipmentIntention.Remove, workRequest))
                .Returns(true);
            _userEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentAssignedToUserAsync(equipmentId, authorId, CancellationToken.None))
                .ReturnsAsync(false);

            await _sut.Invoking(x => x.RemoveEquipmentFromRequestAsync(workRequest,
                    workRequestEquipment.Id, CancellationToken.None))
                .Should().ThrowAsync<EquipmentIsNotAssignedToWorkRequestAuthorException>();
        }

        [Fact]
        public async Task RemoveEquipmentFromRequestAsync_ShouldRemoveEquipmentFromRequest()
        {
            var equipmentId = new Guid("2f4d29b7-2b87-4989-9316-90cd9f8bf6d6");
            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var authorId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                authorId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);
            var workRequestEquipment = workRequest.AddEquipmentToRequest(equipmentId);

            _intentionManagerMock
                .Setup(x => x.IsAllowed(ManageRequestEquipmentIntention.Remove, workRequest))
                .Returns(true);
            _userEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentAssignedToUserAsync(equipmentId, authorId, CancellationToken.None))
                .ReturnsAsync(true);

            await _sut.RemoveEquipmentFromRequestAsync(workRequest, workRequestEquipment.Id, CancellationToken.None);

            workRequest.Equipments.Any(x => x.Id == workRequestEquipment.Id)
                .Should().BeFalse();
        }

        #endregion
    }
}
