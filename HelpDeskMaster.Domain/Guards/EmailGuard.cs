using Ardalis.GuardClauses;
using System.Runtime.CompilerServices;

namespace HelpDeskMaster.Domain.Guards
{
    public static class EmailGuard
    {
        public static void InvalidEmail(this IGuardClause guardClause,
            string value,
            [CallerArgumentExpression("value")] string? parameterName = null)
        {
            try
            {
                new System.Net.Mail.MailAddress(value);
            }
            catch (ArgumentNullException ex) 
            {
                throw new ArgumentNullException(ex.Message, parameterName);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message, parameterName);
            }
            catch (FormatException ex)
            {
                throw new FormatException(ex.Message);
            }
        }
    }
}