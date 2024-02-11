using FluentAssertions;
using HelpDeskMaster.Domain.Authentication;
using HelpDeskMaster.Domain.Entities.Equipments.Intentions;
using Moq;

namespace HelpDeskMaster.Domain.UnitTests.Equipments
{
    public class ManageComputerEquipmentIntentionResolverTests
    {
        private readonly Mock<IIdentity> _identityMock;
        private readonly ManageComputerEquipmentIntentionResolver _sut;

        public ManageComputerEquipmentIntentionResolverTests()
        {
            _identityMock = new Mock<IIdentity>();

            _identityMock
                .Setup(x => x.IsAuthenticated())
                .Returns(true);

            _sut = new ManageComputerEquipmentIntentionResolver();
        }

        #region Assign

        [Fact]
        public void Resolve_ShouldReturnTrue_WhenAssignAndSubjectIsAdmin()
        {
            _identityMock
                .Setup(x => x.IsAdmin())
                .Returns(true);
            _identityMock
                .Setup(x => x.IsHelpDeskMember())
                .Returns(false);

            _sut.Resolve(_identityMock.Object, ManageComputerEquipmentIntention.Assign)
                .Should().BeTrue();
        }

        [Fact]
        public void Resolve_ShouldReturnTrue_WhenAssignAndSubjectIsHelpDeskMember()
        {
            _identityMock
                .Setup(x => x.IsAdmin())
                .Returns(false);
            _identityMock
                .Setup(x => x.IsHelpDeskMember())
                .Returns(true);

            _sut.Resolve(_identityMock.Object, ManageComputerEquipmentIntention.Assign)
                .Should().BeTrue();
        }

        [Fact]
        public void Resolve_ShouldReturnFalse_WhenAssignAndSubjectIsNotAdminAndNotHelpDeskMember()
        {
            _identityMock
                .Setup(x => x.IsAdmin())
                .Returns(false);
            _identityMock
                .Setup(x => x.IsHelpDeskMember())
                .Returns(false);

            _sut.Resolve(_identityMock.Object, ManageComputerEquipmentIntention.Assign)
                .Should().BeFalse();
        }

        #endregion

        #region Unassign

        [Fact]
        public void Resolve_ShouldReturnTrue_WhenUnassignAndSubjectIsAdmin()
        {
            _identityMock
                .Setup(x => x.IsAdmin())
                .Returns(true);
            _identityMock
                .Setup(x => x.IsHelpDeskMember())
                .Returns(false);

            _sut.Resolve(_identityMock.Object, ManageComputerEquipmentIntention.Unassign)
                .Should().BeTrue();
        }

        [Fact]
        public void Resolve_ShouldReturnTrue_WhenUnassignAndSubjectIsHelpDeskMember()
        {
            _identityMock
                .Setup(x => x.IsAdmin())
                .Returns(false);
            _identityMock
                .Setup(x => x.IsHelpDeskMember())
                .Returns(true);

            _sut.Resolve(_identityMock.Object, ManageComputerEquipmentIntention.Unassign)
                .Should().BeTrue();
        }

        [Fact]
        public void Resolve_ShouldReturnFalse_WhenUnassignAndSubjectIsNotAdminAndNotHelpDeskMember()
        {
            _identityMock
                .Setup(x => x.IsAdmin())
                .Returns(false);
            _identityMock
                .Setup(x => x.IsHelpDeskMember())
                .Returns(false);

            _sut.Resolve(_identityMock.Object, ManageComputerEquipmentIntention.Unassign)
                .Should().BeFalse();
        }

        #endregion
    }
}
