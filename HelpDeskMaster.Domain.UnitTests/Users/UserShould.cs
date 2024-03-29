﻿using FluentAssertions;
using HelpDeskMaster.Domain.Entities.Users;

namespace HelpDeskMaster.Domain.UnitTests.Users
{
    public class UserShould
    {
        [Fact]
        public void ThrowArgumentException_WhenLoginIsNull()
        {
            Login? login = null;
            var phoneNumber = "123456789";

            Action Create = () =>
            {
#pragma warning disable CS8604 // Possible null reference argument.
                User.Create(login, phoneNumber);
#pragma warning restore CS8604 // Possible null reference argument.
            };

            FluentActions.Invoking(Create).Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ThrowArgumentException_WhenPhoneNumberIsNull()
        {
            var login = new Login("some.email@domain.com");
            string? phoneNumber = null;

            Action Create = () =>
            {
#pragma warning disable CS8604 // Possible null reference argument.
                User.Create(login, phoneNumber);
#pragma warning restore CS8604 // Possible null reference argument.
            };

            FluentActions.Invoking(Create).Should().Throw<ArgumentException>();
        }
    }
}
