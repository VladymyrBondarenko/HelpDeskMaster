using HelpDeskMaster.Domain.Entities.WorkDirections;
using MediatR;

namespace HelpDeskMaster.App.UseCases.WorkRequest.WorkDirections.GetAllWorkDirections
{
    public record GetAllWorkDirectionsQuery : IRequest<List<WorkDirection>>
    {
    }
}
