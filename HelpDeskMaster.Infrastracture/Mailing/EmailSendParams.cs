namespace HelpDeskMaster.Infrastracture.Mailing
{
    public record EmailSendParams(
        string EmailToId,
        string EmailToName,
        string EmailSubject,
        string EmailBody)
    {
    }
}
