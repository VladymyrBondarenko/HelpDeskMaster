using Hangfire;
using HelpDeskMaster.Infrastracture.BackgroundJobs;

namespace HelpDeskMaster.WebApi.Extensions
{
    public static class BackgroundJobExtensions
    {
        public static IApplicationBuilder UseBackgroundJob(this WebApplication app)
        {
            app.Services
                .GetRequiredService<IRecurringJobManager>()
                .AddOrUpdate<IProcessOutboxMessagesJob>(
                    "outbox-processor", 
                    job => job.ProcessAsync(),
                    app.Configuration["BackgroundJobs:Outbox:Schedule"]);

            return app;
        }
    }
}
