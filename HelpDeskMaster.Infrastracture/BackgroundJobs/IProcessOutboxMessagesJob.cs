
namespace HelpDeskMaster.Infrastracture.BackgroundJobs
{
    public interface IProcessOutboxMessagesJob
    {
        Task ProcessAsync();
    }
}