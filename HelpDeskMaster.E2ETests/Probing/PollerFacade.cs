namespace HelpDeskMaster.E2ETests.Probing
{
    internal class PollerFacade
    {
        public static async Task AssertEventually(IProbe probe, int timeout)
        {
            var poller = new Poller(timeout);

            await poller.CheckAsync(probe);
        }
    }
}