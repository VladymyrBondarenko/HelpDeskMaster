namespace HelpDeskMaster.Infrastracture.BackgroundJobs
{
    public class BackgroundJobsOptions
    {
        public const string Section = "BackgroundJobs";

        public OutboxOptions? Outbox { get; set; }
    }

    public class OutboxOptions
    {
        public string? Schedule { get; set; }

        public int BatchSize { get; set; }
    }
}