using Ardalis.GuardClauses;
using HelpDeskMaster.Domain.Guards;

namespace HelpDeskMaster.Domain.Entities.Users
{
    public record class Login
    {
        public Login(string value)
        {
            Guard.Against.InvalidEmail(value);
            Value = value;
        }

        public string Value { get; private set; }
    }
}