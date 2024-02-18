using HelpDeskMaster.Domain.Entities.WorkDirections;
using MediatR;

namespace HelpDeskMaster.App.UseCases.WorkRequest.WorkDirections.GetAllWorkDirections
{
    public class GetAllWorkDirectionsQuery : IRequest<List<WorkDirection>>
    {
    }
}
