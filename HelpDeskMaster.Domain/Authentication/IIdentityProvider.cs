namespace HelpDeskMaster.Domain.Authentication
{
    public interface IIdentityProvider
    {
        IIdentity Current { get; }
    }
}
