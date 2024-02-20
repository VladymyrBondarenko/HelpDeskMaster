using HelpDeskMaster.Domain.Entities.WorkDirections;
using MediatR;

namespace HelpDeskMaster.App.UseCases.WorkRequest.WorkDirections.CreateWorkDirection
{
    public record class CreateWorkDirectionCommand(string Title) : IRequest<WorkDirection>
    {
    }
}
