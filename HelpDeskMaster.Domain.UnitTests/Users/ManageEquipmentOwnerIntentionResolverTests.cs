using FluentAssertions;
using HelpDeskMaster.Domain.Authentication;
using HelpDeskMaster.Domain.Entities.Equipments.Intentions;
using HelpDeskMaster.Domain.Entities.Users.Intentions;
using Moq;

namespace HelpDeskMaster.Domain.UnitTests.Users
{
    public class ManageEquipmentOwnerIntentionResolverTests
    {
        private readonly Mock<IIdentity> _identityMock;
        private readonly ManageEquipmentOwnerIntentionResolver _sut;

        public ManageEquipmentOwnerIntentionResolverTests()
        {
            _identityMock = new Mock<IIdentity>();

            _identityMock
                .Setup(x => x.IsAuthenticated())
                .Returns(true);

            _sut = new ManageEquipmentOwnerIntentionResolver();
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

            _sut.Resolve(_identityMock.Object, ManageEquipmentOwnerIntention.Assign)
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

            _sut.Resolve(_identityMock.Object, ManageEquipmentOwnerIntention.Assign)
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

            _sut.Resolve(_identityMock.Object, ManageEquipmentOwnerIntention.Assign)
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

            _sut.Resolve(_identityMock.Object, ManageEquipmentOwnerIntention.Unassign)
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

            _sut.Resolve(_identityMock.Object, ManageEquipmentOwnerIntention.Unassign)
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

            _sut.Resolve(_identityMock.Object, ManageEquipmentOwnerIntention.Unassign)
                .Should().BeFalse();
        }

        #endregion
    }
}
