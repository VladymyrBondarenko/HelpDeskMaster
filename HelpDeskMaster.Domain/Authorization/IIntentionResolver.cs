using HelpDeskMaster.Domain.Authentication;

namespace HelpDeskMaster.Domain.Authorization
{
    public interface IIntentionResolver
    {
    }

    public interface IIntentionResolver<in TIntention> : IIntentionResolver
    {
        bool Resolve(IIdentity subject, TIntention intention);
    }

    public interface IIntentionResolver<in TIntention, in TObject> : IIntentionResolver
    {
        bool Resolve(IIdentity subject, TObject @object, TIntention intention);
    }
}