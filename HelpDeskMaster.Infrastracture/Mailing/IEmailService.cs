
namespace HelpDeskMaster.Infrastracture.Mailing
{
    public interface IEmailService
    {
        Task<bool> SendAsync(EmailSendParams sendParams, CancellationToken cancellationToken);
    }
}