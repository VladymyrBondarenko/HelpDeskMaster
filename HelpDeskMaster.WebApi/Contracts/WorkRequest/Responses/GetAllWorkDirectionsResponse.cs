namespace HelpDeskMaster.WebApi.Contracts.WorkRequest.Responses
{
    public class GetAllWorkDirectionsResponse
    {
        public required IReadOnlyList<WorkDirectionModel> WorkDirections { get; set; }
    }
}
