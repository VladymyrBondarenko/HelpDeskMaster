using FluentAssertions;
using HelpDeskMaster.Domain.Authorization;
using HelpDeskMaster.Domain.Entities.Users;
using HelpDeskMaster.Domain.Entities.Users.Intentions;
using HelpDeskMaster.Domain.Exceptions.UserExceptions;
using Moq;

namespace HelpDeskMaster.Domain.UnitTests.Users
{
    public class UserEquipmentServiceTests
    {
        private readonly Mock<IUserEquipmentRepository> _userEquipmentRepositoryMock;
        private readonly Mock<IIntentionManager> _intentionManagerMock;
        private readonly UserEquipmentService _sut;

        public UserEquipmentServiceTests()
        {
            _userEquipmentRepositoryMock = new Mock<IUserEquipmentRepository>();
            _intentionManagerMock = new Mock<IIntentionManager>(); 

            _sut = new UserEquipmentService(_userEquipmentRepositoryMock.Object, _intentionManagerMock.Object);
        }

        #region AssignEquipmentToUserAsync

        [Fact]
        public async Task AssignEquipmentToUserAsync_ShouldAssignEquipmentToUser()
        {
            _intentionManagerMock
                .Setup(x => x.IsAllowed(ManageEquipmentOwnerIntention.Assign))
                .Returns(true);

            var equipmentId = new Guid("cba9efe6-ae1e-4b6b-9227-a11b62f65af7");
            var equipmentTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");
            var assignDate = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            _userEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentAssignedAsync(equipmentId, assignDate, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var user = User.Create(new Login("some@email"), "123456789");

            await _sut.AssignEquipmentToUserAsync(user, equipmentId, assignDate, CancellationToken.None);

            user.Equipments.Should().ContainSingle();
            user.Equipments.First().EquipmentId.Should().Be(equipmentId);
        }

        [Fact]
        public void AssignEquipmentToUserAsync_ShouldThrowIntentionManagerException_WhenForbidden()
        {
            _intentionManagerMock
                .Setup(x => x.IsAllowed(ManageEquipmentOwnerIntention.Assign))
                .Returns(false);

            var equipmentId = new Guid("cba9efe6-ae1e-4b6b-9227-a11b62f65af7");
            var equipmentTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");
            var assignDate = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            _userEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentAssignedAsync(equipmentId, assignDate, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var user = User.Create(new Login("some@email"), "123456789");

            _sut.Invoking(x => x.AssignEquipmentToUserAsync(user, equipmentId, assignDate, CancellationToken.None))
                .Should().ThrowAsync<IntentionManagerException>();
        }

        [Fact]
        public async Task AssignEquipmentToUserAsync_ShouldThrowEquipmentAlreadyAssignedToUserException_WhenAssignAlreadyAssinedEquipmentToUser()
        {
            _intentionManagerMock
                .Setup(x => x.IsAllowed(ManageEquipmentOwnerIntention.Assign))
                .Returns(true);

            var equipmentId = new Guid("cba9efe6-ae1e-4b6b-9227-a11b62f65af7");
            var equipmentTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");
            var assignDate = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            _userEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentAssignedAsync(equipmentId, assignDate, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var user = User.Create(new Login("some@email"), "123456789");

            await _sut.Invoking(x => x.AssignEquipmentToUserAsync(user, equipmentId, assignDate, CancellationToken.None))
                .Should().ThrowAsync<EquipmentAlreadyAssignedToUserException>();
        }

        #endregion

        #region UnassignEquipmentFromUser

        [Fact]
        public void UnassignEquipmentFromUser_ShouldUnassignEquipmentFromUser()
        {
            _intentionManagerMock
                .Setup(x => x.IsAllowed(ManageEquipmentOwnerIntention.Unassign))
                .Returns(true);

            var equipmentId = new Guid("cba9efe6-ae1e-4b6b-9227-a11b62f65af7");
            var equipmentTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");
            var assignDate = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            _userEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentAssignedAsync(equipmentId, assignDate, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var user = User.Create(new Login("some@email"), "123456789");
            user.AssignEquipment(equipmentId, assignDate);

            var unassignDate = new DateTimeOffset(
                2024, 2, 28,
                12, 34, 5,
                new TimeSpan());

            _sut.UnassignEquipmentFromUser(user, equipmentId, unassignDate);

            user.Equipments.First(x => x.EquipmentId == equipmentId)
                .UnassignedDate.Should().Be(unassignDate);
        }

        [Fact]
        public void UnassignEquipmentFromUser_ShouldThrowIntentionManagerException_WhenForbidden()
        {
            _intentionManagerMock
                .Setup(x => x.IsAllowed(ManageEquipmentOwnerIntention.Unassign))
                .Returns(false);

            var equipmentId = new Guid("cba9efe6-ae1e-4b6b-9227-a11b62f65af7");
            var equipmentTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");
            var assignDate = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            _userEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentAssignedAsync(equipmentId, assignDate, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var user = User.Create(new Login("some@email"), "123456789");
            user.AssignEquipment(equipmentId, assignDate);

            var unassignDate = new DateTimeOffset(
                2024, 2, 28,
                12, 34, 5,
                new TimeSpan());

            _sut.Invoking(x => x.UnassignEquipmentFromUser(user, equipmentId, unassignDate))
                .Should().Throw<IntentionManagerException>();
        }

        [Fact]
        public void UnassignEquipmentFromUser_ShouldThrowUserEquipmentNotFoundToUnassignException_WhenEquipmentIsGone()
        {
            _intentionManagerMock
               .Setup(x => x.IsAllowed(ManageEquipmentOwnerIntention.Unassign))
               .Returns(true);

            var equipmentId = new Guid("cba9efe6-ae1e-4b6b-9227-a11b62f65af7");
            var equipmentTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");
            var assignDate = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            _userEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentAssignedAsync(equipmentId, assignDate, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var user = User.Create(new Login("some@email"), "123456789");

            var unassignDate = new DateTimeOffset(
                2024, 2, 28,
                12, 34, 5,
                new TimeSpan());

            _sut.Invoking(x => x.UnassignEquipmentFromUser(user, equipmentId, unassignDate))
                .Should().Throw<UserEquipmentNotFoundToUnassignException>();
        }

        [Fact]
        public void UnassignEquipmentFromUser_ShouldThrowUserEquipmentNotFoundToUnassignException_WhenUnassignDateEarlierThatAssignedDate()
        {
            _intentionManagerMock
               .Setup(x => x.IsAllowed(ManageEquipmentOwnerIntention.Unassign))
               .Returns(true);

            var equipmentId = new Guid("cba9efe6-ae1e-4b6b-9227-a11b62f65af7");
            var equipmentTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");
            var assignDate = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            _userEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentAssignedAsync(equipmentId, assignDate, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var user = User.Create(new Login("some@email"), "123456789");
            user.AssignEquipment(equipmentId, assignDate);

            var unassignDate = new DateTimeOffset(
                2024, 1, 27,
                12, 34, 5,
                new TimeSpan());

            _sut.Invoking(x => x.UnassignEquipmentFromUser(user, equipmentId, unassignDate))
                .Should().Throw<UserEquipmentNotFoundToUnassignException>();
        }

        [Fact]
        public void UnassignEquipmentFromUser_ShouldThrowUserEquipmentNotFoundToUnassignException_WhenEquipmentAlreadyUnassigned()
        {
            _intentionManagerMock
               .Setup(x => x.IsAllowed(ManageEquipmentOwnerIntention.Unassign))
               .Returns(true);

            var equipmentId = new Guid("cba9efe6-ae1e-4b6b-9227-a11b62f65af7");
            var equipmentTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");
            var assignDate = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            _userEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentAssignedAsync(equipmentId, assignDate, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var user = User.Create(new Login("some@email"), "123456789");
            var userEquipment = user.AssignEquipment(equipmentId, assignDate);

            var unassignDateFirst = new DateTimeOffset(
                2024, 2, 28,
                12, 34, 5,
                new TimeSpan());
            userEquipment.UnassignEquipment(unassignDateFirst);

            var unassignDateSecond = new DateTimeOffset(
                2024, 3, 28,
                12, 34, 5,
                new TimeSpan());

            _sut.Invoking(x => x.UnassignEquipmentFromUser(user, equipmentId, unassignDateSecond))
                .Should().Throw<UserEquipmentNotFoundToUnassignException>();
        }

        #endregion
    }
}