namespace HelpDeskMaster.Infrastracture.Mailing
{
    internal record EmailSenderOptions(
        string Server,
        int Port,
        string SenderName,
        string SenderEmail,
        string UserName,
        string Password)
    {
        public static string Section = "EmailSender";
    }
}