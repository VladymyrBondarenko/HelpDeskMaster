namespace HelpDeskMaster.Domain.Authentication
{
    public interface IIdentityProvider
    {
        Task<IIdentity> GetIdentityAsync(CancellationToken cancellationToken);
    }
}
