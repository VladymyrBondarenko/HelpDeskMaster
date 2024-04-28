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
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UserEquipmentService _sut;

        public UserEquipmentServiceTests()
        {
            _userEquipmentRepositoryMock = new Mock<IUserEquipmentRepository>();
            _intentionManagerMock = new Mock<IIntentionManager>();
            _userRepositoryMock = new Mock<IUserRepository>();

            _sut = new UserEquipmentService(
                _userRepositoryMock.Object, 
                _userEquipmentRepositoryMock.Object, 
                _intentionManagerMock.Object);
        }

        #region AssignEquipmentToUserAsync

        [Fact]
        public async Task AssignEquipmentToUserAsync_ShouldAssignEquipmentToUser()
        {
            _intentionManagerMock
                .Setup(x => x.IsAllowedAsync(ManageEquipmentOwnerIntention.Assign, CancellationToken.None))
                .ReturnsAsync(true);

            var equipmentId = new Guid("cba9efe6-ae1e-4b6b-9227-a11b62f65af7");
            var equipmentTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");
            var assignDate = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var userId = new Guid("9797f405-675f-4661-bbc1-7d565deb51c7");
            var user = User.Create(userId, new Login("some@email"), "123456789");

            _userEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentAssignedAsync(equipmentId, assignDate, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            _userRepositoryMock
                .Setup(x => x.GetUserByIdAsync(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            await _sut.AssignEquipmentToUserAsync(userId, equipmentId, assignDate, CancellationToken.None);

            user.Equipments.Should().ContainSingle();
            user.Equipments.First().EquipmentId.Should().Be(equipmentId);
        }

        [Fact]
        public void AssignEquipmentToUserAsync_ShouldThrowIntentionManagerException_WhenForbidden()
        {
            _intentionManagerMock
                .Setup(x => x.IsAllowedAsync(ManageEquipmentOwnerIntention.Assign, CancellationToken.None))
                .ReturnsAsync(false);

            var equipmentId = new Guid("cba9efe6-ae1e-4b6b-9227-a11b62f65af7");
            var equipmentTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");
            var assignDate = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var userId = new Guid("9797f405-675f-4661-bbc1-7d565deb51c7");
            var user = User.Create(userId, new Login("some@email"), "123456789");

            _userEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentAssignedAsync(equipmentId, assignDate, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            _userRepositoryMock
                .Setup(x => x.GetUserByIdAsync(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            _sut.Invoking(x => x.AssignEquipmentToUserAsync(userId, equipmentId, assignDate, CancellationToken.None))
                .Should().ThrowAsync<IntentionManagerException>();
        }

        [Fact]
        public async Task AssignEquipmentToUserAsync_ShouldThrowEquipmentAlreadyAssignedToUserException_WhenAssignAlreadyAssinedEquipmentToUser()
        {
            _intentionManagerMock
                .Setup(x => x.IsAllowedAsync(ManageEquipmentOwnerIntention.Assign, CancellationToken.None))
                .ReturnsAsync(true);

            var equipmentId = new Guid("cba9efe6-ae1e-4b6b-9227-a11b62f65af7");
            var equipmentTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");
            var assignDate = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var userId = new Guid("9797f405-675f-4661-bbc1-7d565deb51c7");
            var user = User.Create(userId, new Login("some@email"), "123456789");

            _userEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentAssignedAsync(equipmentId, assignDate, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            _userRepositoryMock
                .Setup(x => x.GetUserByIdAsync(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            await _sut.Invoking(x => x.AssignEquipmentToUserAsync(userId, equipmentId, assignDate, CancellationToken.None))
                .Should().ThrowAsync<EquipmentAlreadyAssignedToUserException>();
        }

        #endregion

        #region UnassignEquipmentFromUser

        [Fact]
        public async Task UnassignEquipmentFromUser_ShouldUnassignEquipmentFromUser()
        {
            _intentionManagerMock
                .Setup(x => x.IsAllowedAsync(ManageEquipmentOwnerIntention.Unassign, CancellationToken.None))
                .ReturnsAsync(true);

            var equipmentId = new Guid("cba9efe6-ae1e-4b6b-9227-a11b62f65af7");
            var equipmentTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");
            var assignDate = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var userId = new Guid("9797f405-675f-4661-bbc1-7d565deb51c7");
            var user = User.Create(userId, new Login("some@email"), "123456789");

            _userEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentAssignedAsync(equipmentId, assignDate, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            _userRepositoryMock
                .Setup(x => x.GetUserByIdAsync(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            user.AssignEquipment(equipmentId, assignDate);

            var unassignDate = new DateTimeOffset(
                2024, 2, 28,
                12, 34, 5,
                new TimeSpan());

            await _sut.UnassignEquipmentFromUserAsync(userId, equipmentId, unassignDate, CancellationToken.None);

            user.Equipments.First(x => x.EquipmentId == equipmentId)
                .UnassignedDate.Should().Be(unassignDate);
        }

        [Fact]
        public void UnassignEquipmentFromUser_ShouldThrowIntentionManagerException_WhenForbidden()
        {
            _intentionManagerMock
                .Setup(x => x.IsAllowedAsync(ManageEquipmentOwnerIntention.Unassign, CancellationToken.None))
                .ReturnsAsync(false);

            var equipmentId = new Guid("cba9efe6-ae1e-4b6b-9227-a11b62f65af7");
            var equipmentTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");
            var assignDate = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var userId = new Guid("9797f405-675f-4661-bbc1-7d565deb51c7");
            var user = User.Create(userId, new Login("some@email"), "123456789");

            _userEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentAssignedAsync(equipmentId, assignDate, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            _userRepositoryMock
                .Setup(x => x.GetUserByIdAsync(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            user.AssignEquipment(equipmentId, assignDate);

            var unassignDate = new DateTimeOffset(
                2024, 2, 28,
                12, 34, 5,
                new TimeSpan());

            _sut.Invoking(x => x.UnassignEquipmentFromUserAsync(userId, equipmentId, unassignDate, CancellationToken.None))
                .Should().ThrowAsync<IntentionManagerException>();
        }

        [Fact]
        public void UnassignEquipmentFromUser_ShouldThrowUserEquipmentNotFoundToUnassignException_WhenEquipmentIsGone()
        {
            _intentionManagerMock
               .Setup(x => x.IsAllowedAsync(ManageEquipmentOwnerIntention.Unassign, CancellationToken.None))
               .ReturnsAsync(true);

            var equipmentId = new Guid("cba9efe6-ae1e-4b6b-9227-a11b62f65af7");
            var equipmentTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");
            var assignDate = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var userId = new Guid("9797f405-675f-4661-bbc1-7d565deb51c7");
            var user = User.Create(userId, new Login("some@email"), "123456789");

            _userEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentAssignedAsync(equipmentId, assignDate, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            _userRepositoryMock
                .Setup(x => x.GetUserByIdAsync(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            var unassignDate = new DateTimeOffset(
                2024, 2, 28,
                12, 34, 5,
                new TimeSpan());

            _sut.Invoking(x => x.UnassignEquipmentFromUserAsync(userId, equipmentId, unassignDate, CancellationToken.None))
                .Should().ThrowAsync<UserEquipmentNotFoundToUnassignException>();
        }

        [Fact]
        public void UnassignEquipmentFromUser_ShouldThrowUserEquipmentNotFoundToUnassignException_WhenUnassignDateEarlierThatAssignedDate()
        {
            _intentionManagerMock
               .Setup(x => x.IsAllowedAsync(ManageEquipmentOwnerIntention.Unassign, CancellationToken.None))
               .ReturnsAsync(true);

            var equipmentId = new Guid("cba9efe6-ae1e-4b6b-9227-a11b62f65af7");
            var equipmentTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");
            var assignDate = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var userId = new Guid("9797f405-675f-4661-bbc1-7d565deb51c7");
            var user = User.Create(userId, new Login("some@email"), "123456789");

            _userEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentAssignedAsync(equipmentId, assignDate, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            _userRepositoryMock
                .Setup(x => x.GetUserByIdAsync(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            user.AssignEquipment(equipmentId, assignDate);

            var unassignDate = new DateTimeOffset(
                2024, 1, 27,
                12, 34, 5,
                new TimeSpan());

            _sut.Invoking(x => x.UnassignEquipmentFromUserAsync(userId, equipmentId, unassignDate, CancellationToken.None))
                .Should().ThrowAsync<UserEquipmentNotFoundToUnassignException>();
        }

        [Fact]
        public void UnassignEquipmentFromUser_ShouldThrowUserEquipmentNotFoundToUnassignException_WhenEquipmentAlreadyUnassigned()
        {
            _intentionManagerMock
               .Setup(x => x.IsAllowedAsync(ManageEquipmentOwnerIntention.Unassign, CancellationToken.None))
               .ReturnsAsync(true);

            var equipmentId = new Guid("cba9efe6-ae1e-4b6b-9227-a11b62f65af7");
            var equipmentTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");
            var assignDate = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var userId = new Guid("9797f405-675f-4661-bbc1-7d565deb51c7");
            var user = User.Create(userId, new Login("some@email"), "123456789");

            _userEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentAssignedAsync(equipmentId, assignDate, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            _userRepositoryMock
                .Setup(x => x.GetUserByIdAsync(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

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

            _sut.Invoking(x => x.UnassignEquipmentFromUserAsync(userId, equipmentId, unassignDateSecond, CancellationToken.None))
                .Should().ThrowAsync<UserEquipmentNotFoundToUnassignException>();
        }

        #endregion
    }
}