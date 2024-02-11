namespace HelpDeskMaster.Domain.Authorization
{
    public interface IIntentionManager
    {
        bool IsAllowed<TIntention>(TIntention intention) where TIntention : struct;

        bool IsAllowed<TIntention, TObject>(TIntention intention, TObject intentionObject) where TIntention : struct;
    }

    public static class IntentionManagerExtentions
    {
        public static void ThrowIfForbidden<TIntention>(this IIntentionManager intentionManager,
            TIntention intention) where TIntention : struct
        {
            if (!intentionManager.IsAllowed(intention))
            {
                throw new IntentionManagerException();
            }
        }

        public static void ThrowIfForbidden<TIntention, TObject>(this IIntentionManager intentionManager,
            TIntention intention, TObject @object) where TIntention : struct
        {
            if (!intentionManager.IsAllowed(intention, @object))
            {
                throw new IntentionManagerException();
            }
        }
    }
}