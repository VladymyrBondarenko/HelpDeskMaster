namespace HelpDeskMaster.Domain.Authentication
{
    internal class IdentityProvider : IIdentityProvider
    {
        public IIdentity Current => new Identity(new Guid("33c5790f-1da3-4581-9a6b-af81f2233ff6"));
    }
}
