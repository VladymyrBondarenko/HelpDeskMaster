namespace HelpDeskMaster.Domain.Exceptions
{
    public abstract class DomainException : Exception
    {
        public DomainErrorCode ErrorCode { get; }

        public DomainException(DomainErrorCode errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}