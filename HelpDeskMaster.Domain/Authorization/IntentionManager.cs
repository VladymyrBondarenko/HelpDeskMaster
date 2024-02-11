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

        public bool IsAllowed<TIntention>(TIntention intention) where TIntention : struct
        {
            var matchingResolver = _resolvers.OfType<IIntentionResolver<TIntention>>().FirstOrDefault();
            return matchingResolver?.Resolve(_identityProvider.Current, intention) ?? false;
        }

        public bool IsAllowed<TIntention, TObject>(TIntention intention, TObject intentionObject) where TIntention : struct
        {
            var matchingResolver = _resolvers.OfType<IIntentionResolver<TIntention, TObject>>().FirstOrDefault();
            return matchingResolver?.Resolve(_identityProvider.Current, intentionObject, intention) ?? false;
        }
    }
}