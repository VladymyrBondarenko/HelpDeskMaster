using FluentAssertions;
using HelpDeskMaster.Domain.Authentication;
using HelpDeskMaster.Domain.Entities.WorkRequests;
using HelpDeskMaster.Domain.Entities.WorkRequests.Intentions;
using Moq;

namespace HelpDeskMaster.Domain.UnitTests.WorkRequests
{
    public class WorkRequestStageChangeIntentionResolverTests
    {
        private readonly Mock<IIdentity> _identityMock;
        private readonly WorkRequestStageChangeIntentionResolver _sut;

        public WorkRequestStageChangeIntentionResolverTests()
        {
            _identityMock = new Mock<IIdentity>();
            _sut = new WorkRequestStageChangeIntentionResolver();
        }

        #region Move next resolving

        [Fact]
        public void Resolve_ShouldReturnTrue_WhenChangeFromNewRequestToAssignmentAndSubjectIsAdmin()
        {
            _identityMock.Setup(x => x.IsAuthenticated())
                .Returns(true);
            _identityMock.Setup(x => x.IsAdmin())
                .Returns(true);
            _identityMock.Setup(x => x.IsHelpDeskMember())
                .Returns(false);

            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var ownerId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var intention = WorkRequestStageChangeIntention.FromNewRequestToAssignment;
            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                ownerId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);

            _sut.Resolve(_identityMock.Object, workRequest, intention)
                .Should().BeTrue();
        }

        [Fact]
        public void Resolve_ShouldReturnTrue_WhenChangeFromNewRequestToAssignmentAndSubjectIsHelpDeskMember()
        {
            _identityMock.Setup(x => x.IsAuthenticated())
                .Returns(true);
            _identityMock.Setup(x => x.IsAdmin())
                .Returns(false);
            _identityMock.Setup(x => x.IsHelpDeskMember())
                .Returns(true);

            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var ownerId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var intention = WorkRequestStageChangeIntention.FromNewRequestToAssignment;
            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                ownerId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);

            _sut.Resolve(_identityMock.Object, workRequest, intention)
                .Should().BeTrue();
        }

        [Fact]
        public void Resolve_ShouldReturnTrue_WhenChangeFromAssignmentToInWorkAndSubjectIsAdmin()
        {
            _identityMock.Setup(x => x.IsAuthenticated())
                .Returns(true);
            _identityMock.Setup(x => x.IsAdmin())
                .Returns(true);
            _identityMock.Setup(x => x.IsHelpDeskMember())
                .Returns(false);

            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var ownerId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var intention = WorkRequestStageChangeIntention.FromAssignmentToInWork;
            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                ownerId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);

            _sut.Resolve(_identityMock.Object, workRequest, intention)
                .Should().BeTrue();
        }

        [Fact]
        public void Resolve_ShouldReturnTrue_WhenChangeFromAssignmentToInWorkAndSubjectIsHelpDeskMember()
        {
            _identityMock.Setup(x => x.IsAuthenticated())
                .Returns(true);
            _identityMock.Setup(x => x.IsAdmin())
                .Returns(false);
            _identityMock.Setup(x => x.IsHelpDeskMember())
                .Returns(true);

            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var ownerId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var intention = WorkRequestStageChangeIntention.FromAssignmentToInWork;
            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                ownerId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);

            _sut.Resolve(_identityMock.Object, workRequest, intention)
                .Should().BeTrue();
        }

        [Fact]
        public void Resolve_ShouldReturnTrue_WhenChangeFromInWorkToDoneAndSubjectIsAdmin()
        {
            _identityMock.Setup(x => x.IsAuthenticated())
                .Returns(true);
            _identityMock.Setup(x => x.IsAdmin())
                .Returns(true);
            _identityMock.Setup(x => x.IsHelpDeskMember())
                .Returns(false);

            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var ownerId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var intention = WorkRequestStageChangeIntention.FromInWorkToDone;
            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                ownerId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);

            _sut.Resolve(_identityMock.Object, workRequest, intention)
                .Should().BeTrue();
        }

        [Fact]
        public void Resolve_ShouldReturnTrue_WhenChangeFromInWorkToDoneAndSubjectIsHelpMemberAndExecutor()
        {
            var executerId = new Guid("2e7a6329-b32b-4486-a092-6c861872f403");

            _identityMock.Setup(x => x.UserId)
                .Returns(executerId);

            _identityMock.Setup(x => x.IsAuthenticated())
                .Returns(true);
            _identityMock.Setup(x => x.IsAdmin())
                .Returns(false);
            _identityMock.Setup(x => x.IsHelpDeskMember())
                .Returns(true);

            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var ownerId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var intention = WorkRequestStageChangeIntention.FromInWorkToDone;
            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                ownerId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);
            workRequest.AssignExecuterToRequest(executerId);

            _sut.Resolve(_identityMock.Object, workRequest, intention)
                .Should().BeTrue();
        }

        [Fact]
        public void Resolve_ShouldReturnFalse_WhenChangeFromInWorkToDoneAndSubjectIsHelpMemberAndNotExecutor()
        {
            var executerId = new Guid("2e7a6329-b32b-4486-a092-6c861872f403");

            _identityMock.Setup(x => x.UserId)
                .Returns(executerId);

            _identityMock.Setup(x => x.IsAuthenticated())
                .Returns(true);
            _identityMock.Setup(x => x.IsAdmin())
                .Returns(false);
            _identityMock.Setup(x => x.IsHelpDeskMember())
                .Returns(true);

            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var ownerId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var intention = WorkRequestStageChangeIntention.FromInWorkToDone;
            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                ownerId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);

            var differentExecuterId = new Guid("bcf089e2-a4b9-46f5-895f-9df677778fb3");
            workRequest.AssignExecuterToRequest(differentExecuterId);

            _sut.Resolve(_identityMock.Object, workRequest, intention)
                .Should().BeFalse();
        }

        [Fact]
        public void Resolve_ShouldReturnTrue_WhenChangeFromDoneToArhiveAndSubjectIsAdmin()
        {
            _identityMock.Setup(x => x.IsAuthenticated())
                .Returns(true);
            _identityMock.Setup(x => x.IsAdmin())
                .Returns(true);

            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var ownerId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var intention = WorkRequestStageChangeIntention.FromDoneToArhive;
            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                ownerId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);

            _sut.Resolve(_identityMock.Object, workRequest, intention)
                .Should().BeTrue();
        }

        [Fact]
        public void Resolve_ShouldReturnTrue_WhenChangeFromDoneToArhiveAndSubjectIsOwner()
        {
            var ownerId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");

            _identityMock.Setup(x => x.UserId)
                .Returns(ownerId);

            _identityMock.Setup(x => x.IsAuthenticated())
                .Returns(true);
            _identityMock.Setup(x => x.IsAdmin())
                .Returns(false);

            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var intention = WorkRequestStageChangeIntention.FromDoneToArhive;
            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                ownerId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);

            _sut.Resolve(_identityMock.Object, workRequest, intention)
                .Should().BeTrue();
        }

        #endregion

        #region Move back resolving

        [Fact]
        public void Resolve_ShouldReturnTrue_WhenChangeFromArhiveToInWorkAndSubjectIsAdmin()
        {
            _identityMock.Setup(x => x.IsAuthenticated())
                .Returns(true);
            _identityMock.Setup(x => x.IsAdmin())
                .Returns(true);

            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var ownerId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var intention = WorkRequestStageChangeIntention.FromArhiveToInWork;
            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                ownerId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);

            _sut.Resolve(_identityMock.Object, workRequest, intention)
                .Should().BeTrue();
        }

        [Fact]
        public void Resolve_ShouldReturnTrue_WhenChangeFromArhiveToInWorkAndSubjectIsHelpMemberAndExecutor()
        {
            var executorId = new Guid("31946f10-4dfe-4c8d-95cd-2975d8d31f69");

            _identityMock.Setup(x => x.UserId)
                .Returns(executorId);
            _identityMock.Setup(x => x.IsAuthenticated())
                .Returns(true);
            _identityMock.Setup(x => x.IsAdmin())
                .Returns(false);
            _identityMock.Setup(x => x.IsHelpDeskMember())
                .Returns(true);

            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var ownerId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var intention = WorkRequestStageChangeIntention.FromArhiveToInWork;
            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                ownerId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);
            workRequest.AssignExecuterToRequest(executorId);

            _sut.Resolve(_identityMock.Object, workRequest, intention)
                .Should().BeTrue();
        }

        [Fact]
        public void Resolve_ShouldReturnFalse_WhenChangeFromArhiveToInWorkAndSubjectIsHelpMemberAndNotExecutor()
        {
            var executorId = new Guid("31946f10-4dfe-4c8d-95cd-2975d8d31f69");

            _identityMock.Setup(x => x.UserId)
                .Returns(executorId);
            _identityMock.Setup(x => x.IsAuthenticated())
                .Returns(true);
            _identityMock.Setup(x => x.IsAdmin())
                .Returns(false);
            _identityMock.Setup(x => x.IsHelpDeskMember())
                .Returns(true);

            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var ownerId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var intention = WorkRequestStageChangeIntention.FromArhiveToInWork;
            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                ownerId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);

            var differentExecutorId = new Guid("8a7b479b-8860-418c-be79-2492f6a81867");
            workRequest.AssignExecuterToRequest(differentExecutorId);

            _sut.Resolve(_identityMock.Object, workRequest, intention)
                .Should().BeFalse();
        }

        [Fact]
        public void Resolve_ShouldReturnTrue_WhenChangeFromInWorkToAssignmentAndSubjectIsAdmin()
        {
            _identityMock.Setup(x => x.IsAuthenticated())
                .Returns(true);
            _identityMock.Setup(x => x.IsAdmin())
                .Returns(true);

            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var ownerId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var intention = WorkRequestStageChangeIntention.FromInWorkToAssignment;
            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                ownerId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);

            _sut.Resolve(_identityMock.Object, workRequest, intention)
                .Should().BeTrue();
        }

        [Fact]
        public void Resolve_ShouldReturnTrue_WhenChangeFromInWorkToAssignmentAndSubjectIsHelpMemberAndExecutor()
        {
            var executorId = new Guid("31946f10-4dfe-4c8d-95cd-2975d8d31f69");

            _identityMock.Setup(x => x.UserId)
                .Returns(executorId);
            _identityMock.Setup(x => x.IsAuthenticated())
                .Returns(true);
            _identityMock.Setup(x => x.IsAdmin())
                .Returns(false);
            _identityMock.Setup(x => x.IsHelpDeskMember())
                .Returns(true);

            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var ownerId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var intention = WorkRequestStageChangeIntention.FromInWorkToAssignment;
            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                ownerId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);
            workRequest.AssignExecuterToRequest(executorId);

            _sut.Resolve(_identityMock.Object, workRequest, intention)
                .Should().BeTrue();
        }

        [Fact]
        public void Resolve_ShouldReturnFalse_WhenChangeFromInWorkToAssignmentAndSubjectIsHelpMemberAndNotExecutor()
        {
            var executorId = new Guid("31946f10-4dfe-4c8d-95cd-2975d8d31f69");

            _identityMock.Setup(x => x.UserId)
                .Returns(executorId);
            _identityMock.Setup(x => x.IsAuthenticated())
                .Returns(true);
            _identityMock.Setup(x => x.IsAdmin())
                .Returns(false);
            _identityMock.Setup(x => x.IsHelpDeskMember())
                .Returns(true);

            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var ownerId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var intention = WorkRequestStageChangeIntention.FromInWorkToAssignment;
            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                ownerId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);

            var differentExecutorId = new Guid("8a7b479b-8860-418c-be79-2492f6a81867");
            workRequest.AssignExecuterToRequest(differentExecutorId);

            _sut.Resolve(_identityMock.Object, workRequest, intention)
                .Should().BeFalse();
        }

        [Fact]
        public void Resolve_ShouldReturnTrue_WhenChangeFromDoneToInWorkAndSubjectIsAdmin()
        {
            _identityMock.Setup(x => x.IsAuthenticated())
                .Returns(true);
            _identityMock.Setup(x => x.IsAdmin())
                .Returns(true);
            _identityMock.Setup(x => x.IsHelpDeskMember())
                .Returns(false);

            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var ownerId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var intention = WorkRequestStageChangeIntention.FromDoneToInWork;
            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                ownerId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);

            _sut.Resolve(_identityMock.Object, workRequest, intention)
                .Should().BeTrue();
        }

        [Fact]
        public void Resolve_ShouldReturnTrue_WhenChangeFromDoneToInWorkAndSubjectIsOwner()
        {
            var ownerId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");

            _identityMock.Setup(x => x.UserId)
                .Returns(ownerId);
            _identityMock.Setup(x => x.IsAuthenticated())
                .Returns(true);
            _identityMock.Setup(x => x.IsAdmin())
                .Returns(false);
            _identityMock.Setup(x => x.IsHelpDeskMember())
                .Returns(false);

            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var intention = WorkRequestStageChangeIntention.FromDoneToInWork;
            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                ownerId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);

            _sut.Resolve(_identityMock.Object, workRequest, intention)
                .Should().BeTrue();
        }

        [Fact]
        public void Resolve_ShouldReturnTrue_WhenChangeFromDoneToInWorkAndSubjectIsHelpDeskMemberAndExecutor()
        {
            var executorId = new Guid("ed7233c9-7886-4194-b3a2-834e6e6db9dd");

            _identityMock.Setup(x => x.UserId)
                .Returns(executorId);
            _identityMock.Setup(x => x.IsAuthenticated())
                .Returns(true);
            _identityMock.Setup(x => x.IsAdmin())
                .Returns(false);
            _identityMock.Setup(x => x.IsHelpDeskMember())
                .Returns(true);

            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var ownerId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var intention = WorkRequestStageChangeIntention.FromDoneToInWork;
            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                ownerId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);
            workRequest.AssignExecuterToRequest(executorId);

            _sut.Resolve(_identityMock.Object, workRequest, intention)
                .Should().BeTrue();
        }

        [Fact]
        public void Resolve_ShouldReturnFalse_WhenChangeFromDoneToInWorkAndSubjectIsHelpDeskMemberAndNotExecutor()
        {
            var executorId = new Guid("ed7233c9-7886-4194-b3a2-834e6e6db9dd");

            _identityMock.Setup(x => x.UserId)
                .Returns(executorId);
            _identityMock.Setup(x => x.IsAuthenticated())
                .Returns(true);
            _identityMock.Setup(x => x.IsAdmin())
                .Returns(false);
            _identityMock.Setup(x => x.IsHelpDeskMember())
                .Returns(true);

            var workRequestId = new Guid("1f7321f3-f858-41a2-a83c-60f9b8893f03");
            var ownerId = new Guid("88d5d128-b202-4c52-b7d1-82aea236604e");
            var directionId = new Guid("f9242609-2df0-4cf6-ad5c-67a7c099a87d");
            var categoryId = new Guid("55778c0d-eb4f-4ada-a840-400455623033");
            var createdAt = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var intention = WorkRequestStageChangeIntention.FromDoneToInWork;
            var workRequest = WorkRequest.Create(workRequestId,
                createdAt,
                ownerId,
                directionId,
                categoryId,
                createdAt,
                createdAt,
                null);

            var differentExecuterId = new Guid("37327fcb-7666-4b47-aa64-de34dd3c7679");
            workRequest.AssignExecuterToRequest(differentExecuterId);

            _sut.Resolve(_identityMock.Object, workRequest, intention)
                .Should().BeFalse();
        }

        #endregion
    }
}