using FluentAssertions;
using HelpDeskMaster.Domain.Entities.Users;

namespace HelpDeskMaster.Domain.UnitTests.Users
{
    public class LoginShould
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ThrowArgumentException_WhenLoginIsNullOrEmpty(string value)
        {
            Login Create() => new Login(value);

            FluentActions.Invoking(Create).Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData("some text")]
        [InlineData("sometext@")]
        public void ThrowFormatException_WhenLoginIsInvalid(string value)
        {
            Login Create() => new Login(value);

            FluentActions.Invoking(Create).Should().Throw<FormatException>();
        }

        [Theory]
        [InlineData("some.user1@somedomain.com")]
        [InlineData("some@email")]
        public void NotThrowAnyException_WhenLoginIsvalid(string value)
        {
            Login Create() => new Login(value);

            FluentActions.Invoking(Create).Should().NotThrow();
        }
    }
}