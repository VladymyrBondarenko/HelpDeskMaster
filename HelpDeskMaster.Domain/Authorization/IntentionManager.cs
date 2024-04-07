using HelpDeskMaster.Domain.Authentication;

namespace HelpDeskMaster.Domain.Authorization
{
    internal class IntentionManager : IIntentionManager
    {
        private readonly IEnumerable<IIntentionResolver> _resolvers;
        private readonly IIdentityProvider _identityProvider;

        public IntentionManager(IEnumerable<IIntentionResolver> resolvers,
            IIdentityProvider identityProvider)
        {
            _resolvers = resolvers;
            _identityProvider = identityProvider;
        }

        public async Task<bool> IsAllowedAsync<TIntention>(TIntention intention, 
            CancellationToken cancellationToken) where TIntention : struct
        {
            var currentIdentity = await _identityProvider.GetIdentityAsync(cancellationToken);

            var matchingResolver = _resolvers.OfType<IIntentionResolver<TIntention>>().FirstOrDefault();
            return matchingResolver?.Resolve(currentIdentity, intention) ?? false;
        }

        public async Task<bool> IsAllowedAsync<TIntention, TObject>(TIntention intention, TObject intentionObject, 
            CancellationToken cancellationToken) where TIntention : struct
        {
            var currentIdentity = await _identityProvider.GetIdentityAsync(cancellationToken);

            var matchingResolver = _resolvers.OfType<IIntentionResolver<TIntention, TObject>>().FirstOrDefault();
            return matchingResolver?.Resolve(currentIdentity, intentionObject, intention) ?? false;
        }
    }
}