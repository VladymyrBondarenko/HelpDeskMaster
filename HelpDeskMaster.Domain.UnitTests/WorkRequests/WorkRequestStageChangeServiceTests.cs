using FluentAssertions;
using HelpDeskMaster.Domain.Authorization;
using HelpDeskMaster.Domain.Entities.WorkRequests;
using HelpDeskMaster.Domain.Entities.WorkRequests.Intentions;
using HelpDeskMaster.Domain.Exceptions.WorkRequestExceptions;
using Moq;

namespace HelpDeskMaster.Domain.UnitTests.WorkRequests
{
    public class WorkRequestStageChangeServiceTests
    {
        private readonly Mock<IIntentionManager> _intentionManagerMock;
        private readonly WorkRequestStageChangeService _sut;

        public WorkRequestStageChangeServiceTests()
        {
            _intentionManagerMock = new Mock<IIntentionManager>();
            _sut = new WorkRequestStageChangeService(_intentionManagerMock.Object);
        }

        #region MoveRequestStageNext

        [Fact]
        public void MoveRequestStageNext_ShouldChangeWorkRequestStageToAssignment_WhenMoveNextOnNewRequestStage()
        {
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

            _intentionManagerMock
                .Setup(x => x.IsAllowed(It.IsAny<WorkRequestStageChangeIntention>(), workRequest))
                .Returns(true);

            // NewRequest -> Assignment
            _sut.MoveRequestStageNext(workRequest);

            workRequest.RequestStageChanges.Should().HaveCount(2);
            workRequest.RequestStageChanges[0].Stage.Should().Be(WorkRequestStage.NewRequest);
            workRequest.RequestStageChanges[1].Stage.Should().Be(WorkRequestStage.Assignment);
        }

        [Fact]
        public void MoveRequestStageNext_ShouldChangeWorkRequestStageToInWork_WhenMoveNextOnAssignmentStage()
        {
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

            _intentionManagerMock
                .Setup(x => x.IsAllowed(It.IsAny<WorkRequestStageChangeIntention>(), workRequest))
                .Returns(true);

            // NewRequest -> Assignment
            _sut.MoveRequestStageNext(workRequest);

            // Assignment -> InWork
            _sut.MoveRequestStageNext(workRequest);

            workRequest.RequestStageChanges.Should().HaveCount(3);
            workRequest.RequestStageChanges[0].Stage.Should().Be(WorkRequestStage.NewRequest);
            workRequest.RequestStageChanges[1].Stage.Should().Be(WorkRequestStage.Assignment);
            workRequest.RequestStageChanges[2].Stage.Should().Be(WorkRequestStage.InWork);
        }

        [Fact]
        public void MoveRequestStageNext_ShouldChangeWorkRequestStageToDone_WhenMoveNextOnInWorkStage()
        {
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

            _intentionManagerMock
                .Setup(x => x.IsAllowed(It.IsAny<WorkRequestStageChangeIntention>(), workRequest))
                .Returns(true);

            // NewRequest -> Assignment
            _sut.MoveRequestStageNext(workRequest);

            // Assignment -> InWork
            _sut.MoveRequestStageNext(workRequest);

            // InWork -> Done
            _sut.MoveRequestStageNext(workRequest);

            workRequest.RequestStageChanges.Should().HaveCount(4);
            workRequest.RequestStageChanges[0].Stage.Should().Be(WorkRequestStage.NewRequest);
            workRequest.RequestStageChanges[1].Stage.Should().Be(WorkRequestStage.Assignment);
            workRequest.RequestStageChanges[2].Stage.Should().Be(WorkRequestStage.InWork);
            workRequest.RequestStageChanges[3].Stage.Should().Be(WorkRequestStage.Done);
        }

        [Fact]
        public void MoveRequestStageNext_ShouldChangeWorkRequestStageToArchive_WhenMoveNextOnDoneStage()
        {
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

            _intentionManagerMock
                .Setup(x => x.IsAllowed(It.IsAny<WorkRequestStageChangeIntention>(), workRequest))
                .Returns(true);

            // NewRequest -> Assignment
            _sut.MoveRequestStageNext(workRequest);

            // Assignment -> InWork
            _sut.MoveRequestStageNext(workRequest);

            // InWork -> Done
            _sut.MoveRequestStageNext(workRequest);

            // Done -> Arhive
            _sut.MoveRequestStageNext(workRequest);

            workRequest.RequestStageChanges.Should().HaveCount(5);
            workRequest.RequestStageChanges[0].Stage.Should().Be(WorkRequestStage.NewRequest);
            workRequest.RequestStageChanges[1].Stage.Should().Be(WorkRequestStage.Assignment);
            workRequest.RequestStageChanges[2].Stage.Should().Be(WorkRequestStage.InWork);
            workRequest.RequestStageChanges[3].Stage.Should().Be(WorkRequestStage.Done);
            workRequest.RequestStageChanges[4].Stage.Should().Be(WorkRequestStage.Archive);
        }

        [Fact]
        public void MoveRequestStageNext_ShouldWorkRequestNextStageResolvingException_WhenMoveNextOnArhiveStage()
        {
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

            _intentionManagerMock
                .Setup(x => x.IsAllowed(It.IsAny<WorkRequestStageChangeIntention>(), workRequest))
                .Returns(true);

            // NewRequest -> Assignment
            _sut.MoveRequestStageNext(workRequest);

            // Assignment -> InWork
            _sut.MoveRequestStageNext(workRequest);

            // InWork -> Done
            _sut.MoveRequestStageNext(workRequest);

            // Done -> Arhive
            _sut.MoveRequestStageNext(workRequest);

            // Invalid move next
            _sut.Invoking(x => x.MoveRequestStageNext(workRequest))
                .Should()
                .Throw<WorkRequestNextStageResolvingException>();
        }

        #endregion

        #region MoveRequestStageBack

        [Fact]
        public void MoveRequestStageBack_ShouldChangeWorkRequestStageToAssignment_WhenMoveBackOnInWorkStage()
        {
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

            _intentionManagerMock
                .Setup(x => x.IsAllowed(It.IsAny<WorkRequestStageChangeIntention>(), workRequest))
                .Returns(true);

            // NewRequest -> Assignment
            _sut.MoveRequestStageNext(workRequest);

            // Assignment -> InWork
            _sut.MoveRequestStageNext(workRequest);

            // InWork -> Assignment
            _sut.MoveRequestStageBack(workRequest);

            workRequest.RequestStageChanges.Should().HaveCount(4);
            workRequest.RequestStageChanges[0].Stage.Should().Be(WorkRequestStage.NewRequest);
            workRequest.RequestStageChanges[1].Stage.Should().Be(WorkRequestStage.Assignment);
            workRequest.RequestStageChanges[2].Stage.Should().Be(WorkRequestStage.InWork);
            workRequest.RequestStageChanges[3].Stage.Should().Be(WorkRequestStage.Assignment);
        }

        [Fact]
        public void MoveRequestStageBack_ShouldChangeWorkRequestStageToInWork_WhenMoveBackOnDoneStage()
        {
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

            _intentionManagerMock
                .Setup(x => x.IsAllowed(It.IsAny<WorkRequestStageChangeIntention>(), workRequest))
                .Returns(true);

            // NewRequest -> Assignment
            _sut.MoveRequestStageNext(workRequest);

            // Assignment -> InWork
            _sut.MoveRequestStageNext(workRequest);

            // InWork -> Done
            _sut.MoveRequestStageNext(workRequest);

            // Done -> InWork
            _sut.MoveRequestStageBack(workRequest);

            workRequest.RequestStageChanges.Should().HaveCount(5);
            workRequest.RequestStageChanges[0].Stage.Should().Be(WorkRequestStage.NewRequest);
            workRequest.RequestStageChanges[1].Stage.Should().Be(WorkRequestStage.Assignment);
            workRequest.RequestStageChanges[2].Stage.Should().Be(WorkRequestStage.InWork);
            workRequest.RequestStageChanges[3].Stage.Should().Be(WorkRequestStage.Done);
            workRequest.RequestStageChanges[4].Stage.Should().Be(WorkRequestStage.InWork);
        }

        [Fact]
        public void MoveRequestStageBack_ShouldChangeWorkRequestStageToInWork_WhenMoveBackOnArchiveStage()
        {
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

            _intentionManagerMock
                .Setup(x => x.IsAllowed(It.IsAny<WorkRequestStageChangeIntention>(), workRequest))
                .Returns(true);

            // NewRequest -> Assignment
            _sut.MoveRequestStageNext(workRequest);

            // Assignment -> InWork
            _sut.MoveRequestStageNext(workRequest);

            // InWork -> Done
            _sut.MoveRequestStageNext(workRequest);

            // Done -> Archive
            _sut.MoveRequestStageNext(workRequest);

            // Archive -> InWork
            _sut.MoveRequestStageBack(workRequest);

            workRequest.RequestStageChanges.Should().HaveCount(6);
            workRequest.RequestStageChanges[0].Stage.Should().Be(WorkRequestStage.NewRequest);
            workRequest.RequestStageChanges[1].Stage.Should().Be(WorkRequestStage.Assignment);
            workRequest.RequestStageChanges[2].Stage.Should().Be(WorkRequestStage.InWork);
            workRequest.RequestStageChanges[3].Stage.Should().Be(WorkRequestStage.Done);
            workRequest.RequestStageChanges[4].Stage.Should().Be(WorkRequestStage.Archive);
            workRequest.RequestStageChanges[5].Stage.Should().Be(WorkRequestStage.InWork);
        }

        [Fact]
        public void MoveRequestStageBack_ShouldThrowWorkRequestPreviousStageResolvingException_WhenMoveBackOnAssignmentStage()
        {
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

            _intentionManagerMock
                .Setup(x => x.IsAllowed(It.IsAny<WorkRequestStageChangeIntention>(), workRequest))
                .Returns(true);

            // NewRequest -> Assignment
            _sut.MoveRequestStageNext(workRequest);

            // Invalid move back
            _sut.Invoking(x => x.MoveRequestStageBack(workRequest))
                .Should().Throw<WorkRequestPreviousStageResolvingException>();
        }

        #endregion
    }
}