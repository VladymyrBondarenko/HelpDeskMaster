namespace HelpDeskMaster.Domain.Authorization
{
    public interface IIntentionManager
    {
        Task<bool> IsAllowedAsync<TIntention>(TIntention intention,
            CancellationToken cancellationToken) where TIntention : struct;

        Task<bool> IsAllowedAsync<TIntention, TObject>(TIntention intention, TObject intentionObject,
            CancellationToken cancellationToken) where TIntention : struct;
    }

    public static class IntentionManagerExtentions
    {
        public async static Task ThrowIfForbiddenAsync<TIntention>(
            this IIntentionManager intentionManager, TIntention intention, 
            CancellationToken cancellationToken) where TIntention : struct
        {
            if (!(await intentionManager.IsAllowedAsync(intention, cancellationToken)))
            {
                throw new IntentionManagerException();
            }
        }

        public async static Task ThrowIfForbiddenAsync<TIntention, TObject>(
            this IIntentionManager intentionManager, TIntention intention, TObject @object, 
            CancellationToken cancellationToken) where TIntention : struct
        {
            if (!(await intentionManager.IsAllowedAsync(intention, @object, cancellationToken)))
            {
                throw new IntentionManagerException();
            }
        }
    }
}