using FluentAssertions;
using HelpDeskMaster.Domain.Authorization;
using HelpDeskMaster.Domain.Entities.Equipments;
using HelpDeskMaster.Domain.Entities.Equipments.Intentions;
using HelpDeskMaster.Domain.Exceptions.EquipmentExceptions;
using Moq;

namespace HelpDeskMaster.Domain.UnitTests.Equipments
{
    public class ComputerEquipmentServiceTests
    {
        private readonly Mock<IComputerEquipmentRepository> _computerEquipmentRepositoryMock;
        private readonly Mock<IIntentionManager> _intentionManagerMock;
        private readonly ComputerEquipmentService _sut;

        public ComputerEquipmentServiceTests()
        {
            _computerEquipmentRepositoryMock = new Mock<IComputerEquipmentRepository>();
            _intentionManagerMock = new Mock<IIntentionManager>();

            _sut = new ComputerEquipmentService(_computerEquipmentRepositoryMock.Object, _intentionManagerMock.Object);
        }

        #region AssignEquipmentToComputerAsync

        [Fact]
        public async Task AssignEquipmentToComputerAsync_ShouldAssignEquipmentToComputer()
        {
            _intentionManagerMock
                .Setup(x => x.IsAllowedAsync(ManageComputerEquipmentIntention.Assign, CancellationToken.None))
                .ReturnsAsync(true);

            var equipmentComputerTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");
            var equipmentTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");

            var computerId = new Guid("33d6d1f1-fa56-4200-9da7-e2a2d578d134");
            var equipmentId = new Guid("cba9efe6-ae1e-4b6b-9227-a11b62f65af7");
            var assignDate = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5, 
                new TimeSpan());

            var computer = new Equipment(
               computerId,
               equipmentComputerTypeId,
               model: null,
               assignDate,
               factoryNumber: null,
               0,
               departmentId: null,
               assignDate);

            _computerEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentComputerAsync(computerId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            _computerEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentComputerAsync(equipmentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            _computerEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentAssignedAsync(equipmentId, assignDate, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            await _sut.AssignEquipmentToComputerAsync(computer, equipmentId, assignDate, CancellationToken.None);

            computer.ComputerEquipments.Should().ContainSingle();
            computer.ComputerEquipments.First().EquipmentId.Should().Be(equipmentId);
        }

        [Fact]
        public async Task AssignEquipmentToComputerAsync_ShouldThrowIntentionManagerException_WhenForbidden()
        {
            _intentionManagerMock
                .Setup(x => x.IsAllowedAsync(ManageComputerEquipmentIntention.Assign, CancellationToken.None))
                .ReturnsAsync(false);

            var equipmentComputerTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");
            var equipmentTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");

            var computerId = new Guid("33d6d1f1-fa56-4200-9da7-e2a2d578d134");
            var equipmentId = new Guid("cba9efe6-ae1e-4b6b-9227-a11b62f65af7");
            var assignDate = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var computer = new Equipment(
               computerId,
               equipmentComputerTypeId,
               model: null,
               assignDate,
               factoryNumber: null,
               0,
               departmentId: null,
               assignDate);

            _computerEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentComputerAsync(computerId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            _computerEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentComputerAsync(equipmentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            _computerEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentAssignedAsync(equipmentId, assignDate, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            await _sut.Invoking(x => x.AssignEquipmentToComputerAsync(computer, equipmentId, assignDate, CancellationToken.None))
                .Should().ThrowAsync<IntentionManagerException>();
        }

        [Fact]
        public async Task AssignEquipmentToComputerAsync_ShouldThrowAssignComputerToItselfException_WhenAssignComputerToItself()
        {
            _intentionManagerMock
                .Setup(x => x.IsAllowedAsync(ManageComputerEquipmentIntention.Assign, CancellationToken.None))
                .ReturnsAsync(true);

            var equipmentComputerTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");
            var equipmentTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");

            var computerId = new Guid("33d6d1f1-fa56-4200-9da7-e2a2d578d134");
            var assignDate = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var computer = new Equipment(
               computerId,
               equipmentComputerTypeId,
               model: null,
               assignDate,
               factoryNumber: null,
               0,
               departmentId: null,
               assignDate);

            await _sut.Invoking(x => x.AssignEquipmentToComputerAsync(computer, computerId, assignDate, CancellationToken.None))
                .Should().ThrowAsync<AssignComputerToItselfException>();
        }

        [Fact]
        public async Task AssignEquipmentToComputerAsync_ShouldThrowAssignEquipmentNotToComputerException_WhenAssignEquipmentNotToComputer()
        {
            _intentionManagerMock
                .Setup(x => x.IsAllowedAsync(ManageComputerEquipmentIntention.Assign, CancellationToken.None))
                .ReturnsAsync(true);

            var equipmentComputerTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");
            var equipmentTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");

            var computerId = new Guid("33d6d1f1-fa56-4200-9da7-e2a2d578d134");
            var equipmentId = new Guid("cba9efe6-ae1e-4b6b-9227-a11b62f65af7");
            var assignDate = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var computer = new Equipment(
               computerId,
               equipmentComputerTypeId,
               model: null,
               assignDate,
               factoryNumber: null,
               0,
               departmentId: null,
               assignDate);

            _computerEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentComputerAsync(computerId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            await _sut.Invoking(x => x.AssignEquipmentToComputerAsync(computer, equipmentId, assignDate, CancellationToken.None))
                .Should().ThrowAsync<AssignEquipmentNotToComputerException>();
        }

        [Fact]
        public async Task AssignEquipmentToComputerAsync_ShouldThrowAssignToComputerNotEquipmentException_WhenAssignToComputerNotEquipment()
        {
            _intentionManagerMock
                .Setup(x => x.IsAllowedAsync(ManageComputerEquipmentIntention.Assign, CancellationToken.None))
                .ReturnsAsync(true);

            var equipmentComputerTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");
            var equipmentTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");

            var computerId = new Guid("33d6d1f1-fa56-4200-9da7-e2a2d578d134");
            var equipmentId = new Guid("cba9efe6-ae1e-4b6b-9227-a11b62f65af7");
            var assignDate = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var computer = new Equipment(
               computerId,
               equipmentComputerTypeId,
               model: null,
               assignDate,
               factoryNumber: null,
               0,
               departmentId: null,
               assignDate);

            _computerEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentComputerAsync(computerId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            _computerEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentComputerAsync(equipmentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            await _sut.Invoking(x => x.AssignEquipmentToComputerAsync(computer, equipmentId, assignDate, CancellationToken.None))
                .Should().ThrowAsync<AssignToComputerNotEquipmentException>();
        }

        [Fact]
        public async Task AssignEquipmentToComputerAsync_ThrowEquipmentAlreadyAssignedToComputerException_WhenAssignToComputerAlreadyAssignedEquipment()
        {
            _intentionManagerMock
                .Setup(x => x.IsAllowedAsync(ManageComputerEquipmentIntention.Assign, CancellationToken.None))
                .ReturnsAsync(true);

            var equipmentComputerTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");
            var equipmentTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");

            var computerId = new Guid("33d6d1f1-fa56-4200-9da7-e2a2d578d134");
            var equipmentId = new Guid("cba9efe6-ae1e-4b6b-9227-a11b62f65af7");
            var assignDate = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var computer = new Equipment(
               computerId,
               equipmentComputerTypeId,
               model: null,
               assignDate,
               factoryNumber: null,
               0,
               departmentId: null,
               assignDate);

            _computerEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentComputerAsync(computerId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            _computerEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentComputerAsync(equipmentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            _computerEquipmentRepositoryMock
                .Setup(x => x.IsEquipmentAssignedAsync(equipmentId, assignDate, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            await _sut.Invoking(x => x.AssignEquipmentToComputerAsync(computer, equipmentId, assignDate, CancellationToken.None))
                .Should().ThrowAsync<EquipmentAlreadyAssignedToComputerException>();
        }

        #endregion

        #region UnassignEquipmentFromComputer

        [Fact]
        public async Task UnassignEquipmentFromComputer_ShouldUnassignEquipmentFromComputer()
        {
            _intentionManagerMock
                .Setup(x => x.IsAllowedAsync(ManageComputerEquipmentIntention.Unassign, CancellationToken.None))
                .ReturnsAsync(true);

            var equipmentComputerTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");
            var equipmentTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");

            var computerId = new Guid("33d6d1f1-fa56-4200-9da7-e2a2d578d134");
            var equipmentId = new Guid("cba9efe6-ae1e-4b6b-9227-a11b62f65af7");
            var assignDate = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var computer = new Equipment(
               computerId,
               equipmentComputerTypeId,
               model: null,
               assignDate,
               factoryNumber: null,
               0,
               departmentId: null,
               assignDate);

            computer.AssignEquipmentToComputer(equipmentId, assignDate);

            var unassignDate = new DateTimeOffset(
                2024, 2, 28,
                12, 34, 5,
                new TimeSpan());

            await _sut.UnassignEquipmentFromComputerAsync(computer, equipmentId, unassignDate, CancellationToken.None);

            computer.ComputerEquipments.First(x => x.EquipmentId == equipmentId)
                .UnassignedDate.Should().Be(unassignDate);
        }

        [Fact]
        public void UnassignEquipmentFromComputer_ShouldThrowIntentionManagerException_WhenForbidden()
        {
            _intentionManagerMock
                .Setup(x => x.IsAllowedAsync(ManageComputerEquipmentIntention.Unassign, CancellationToken.None))
                .ReturnsAsync(false);

            var equipmentComputerTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");
            var equipmentTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");

            var computerId = new Guid("33d6d1f1-fa56-4200-9da7-e2a2d578d134");
            var equipmentId = new Guid("cba9efe6-ae1e-4b6b-9227-a11b62f65af7");
            var assignDate = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var computer = new Equipment(
               computerId,
               equipmentComputerTypeId,
               model: null,
               assignDate,
               factoryNumber: null,
               0,
               departmentId: null,
               assignDate);

            computer.AssignEquipmentToComputer(equipmentId, assignDate);

            var unassignDate = new DateTimeOffset(
                2024, 2, 28,
                12, 34, 5,
                new TimeSpan());

            _sut.Invoking(x => x.UnassignEquipmentFromComputerAsync(computer, equipmentId, unassignDate, CancellationToken.None))
                .Should().ThrowAsync<IntentionManagerException>();
        }

        [Fact]
        public void UnassignEquipmentFromComputer_ShouldThrowComputerEquipmentNotFoundToUnassignException_WhenComputerEquipmentIsGone()
        {
            _intentionManagerMock
                .Setup(x => x.IsAllowedAsync(ManageComputerEquipmentIntention.Unassign, CancellationToken.None))
                .ReturnsAsync(true);

            var equipmentComputerTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");
            var equipmentTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");

            var computerId = new Guid("33d6d1f1-fa56-4200-9da7-e2a2d578d134");
            var equipmentId = new Guid("cba9efe6-ae1e-4b6b-9227-a11b62f65af7");
            var assignDate = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var computer = new Equipment(
               computerId,
               equipmentComputerTypeId,
               model: null,
               assignDate,
               factoryNumber: null,
               0,
               departmentId: null,
               assignDate);

            var unassignDate = new DateTimeOffset(
                2024, 2, 28,
                12, 34, 5,
                new TimeSpan());

            _sut.Invoking(x => x.UnassignEquipmentFromComputerAsync(computer, equipmentId, unassignDate, CancellationToken.None))
                .Should().ThrowAsync<ComputerEquipmentNotFoundToUnassignException>();
        }

        [Fact]
        public void UnassignEquipmentFromComputer_ShouldThrowComputerEquipmentNotFoundToUnassignException_WhenUnassignDateEarlierThatAssignedDate()
        {
            _intentionManagerMock
                .Setup(x => x.IsAllowedAsync(ManageComputerEquipmentIntention.Unassign, CancellationToken.None))
                .ReturnsAsync(true);

            var equipmentComputerTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");
            var equipmentTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");

            var computerId = new Guid("33d6d1f1-fa56-4200-9da7-e2a2d578d134");
            var equipmentId = new Guid("cba9efe6-ae1e-4b6b-9227-a11b62f65af7");
            var assignDate = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var computer = new Equipment(
               computerId,
               equipmentComputerTypeId,
               model: null,
               assignDate,
               factoryNumber: null,
               0,
               departmentId: null,
               assignDate);

            computer.AssignEquipmentToComputer(equipmentId, assignDate);

            var unassignDate = new DateTimeOffset(
                2024, 1, 27,
                12, 34, 5,
                new TimeSpan());

            _sut.Invoking(x => x.UnassignEquipmentFromComputerAsync(computer, equipmentId, unassignDate, CancellationToken.None))
                .Should().ThrowAsync<ComputerEquipmentNotFoundToUnassignException>();
        }

        [Fact]
        public void UnassignEquipmentFromComputer_ShouldThrowComputerEquipmentNotFoundToUnassignException_WhenEquipmentAlreadyUnassigned()
        {
            _intentionManagerMock
                .Setup(x => x.IsAllowedAsync(ManageComputerEquipmentIntention.Unassign, CancellationToken.None))
                .ReturnsAsync(true);

            var equipmentComputerTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");
            var equipmentTypeId = new Guid("aaca677b-5ad4-474c-8571-bf49c036ec6c");

            var computerId = new Guid("33d6d1f1-fa56-4200-9da7-e2a2d578d134");
            var equipmentId = new Guid("cba9efe6-ae1e-4b6b-9227-a11b62f65af7");
            var assignDate = new DateTimeOffset(
                2024, 1, 28,
                12, 34, 5,
                new TimeSpan());

            var computer = new Equipment(
               computerId,
               equipmentComputerTypeId,
               model: null,
               assignDate,
               factoryNumber: null,
               0,
               departmentId: null,
               assignDate);
            var computerEquipment = computer.AssignEquipmentToComputer(equipmentId, assignDate);

            var unassignDateFirst = new DateTimeOffset(
                2024, 2, 28,
                12, 34, 5,
                new TimeSpan());
            computerEquipment.UnassignEquipmentFromComputer(unassignDateFirst);

            var unassignDateSecond = new DateTimeOffset(
                2024, 2, 29,
                12, 34, 5,
                new TimeSpan());

            _sut.Invoking(x => x.UnassignEquipmentFromComputerAsync(computer, equipmentId, unassignDateSecond, CancellationToken.None))
                .Should().ThrowAsync<ComputerEquipmentNotFoundToUnassignException>();
        }

        #endregion
    }
}