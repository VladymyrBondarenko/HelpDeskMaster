using MediatR;

namespace HelpDeskMaster.App.UseCases.WorkRequest.WorkDirections.DeleteWorkDirection
{
    public record class DeleteWorkDirectionCommand(Guid WorkDirectionId) : IRequest
    {
    }
}