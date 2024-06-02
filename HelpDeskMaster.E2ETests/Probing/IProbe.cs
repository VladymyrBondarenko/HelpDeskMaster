namespace HelpDeskMaster.E2ETests.Probing
{
    internal interface IProbe
    {
        bool IsSatisfied();

        Task SampleAsync();

        string DescribeFailureTo();
    }

    internal interface IProbe<T>
    {
        bool IsSatisfied(T? sample);

        Task<T> GetSampleAsync();

        string DescribeFailureTo();
    }
}
