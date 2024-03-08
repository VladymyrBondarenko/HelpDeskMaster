namespace HelpDeskMaster.Infrastracture.Exceptions
{
    internal class AuthenticationException : Exception
    {
        public AuthenticationException()
            : base("Exception occured while trying resolve identity")
        {

        }
    }
}