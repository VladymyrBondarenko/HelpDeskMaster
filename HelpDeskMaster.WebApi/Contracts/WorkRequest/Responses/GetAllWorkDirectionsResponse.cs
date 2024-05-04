namespace HelpDeskMaster.WebApi.Contracts.WorkRequest.Responses
{
    public record GetAllWorkDirectionsResponse(
        IReadOnlyList<WorkDirectionModel> WorkDirections)
    {
    }
}
