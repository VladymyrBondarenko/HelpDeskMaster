using HelpDeskMaster.Domain.Exceptions;

namespace HelpDeskMaster.Domain.Authorization
{
    public class IntentionManagerException : DomainException
    {
        public IntentionManagerException() 
            : base(DomainErrorCode.Forbidden, "Action is not allowed")
        {
        }
    }
}