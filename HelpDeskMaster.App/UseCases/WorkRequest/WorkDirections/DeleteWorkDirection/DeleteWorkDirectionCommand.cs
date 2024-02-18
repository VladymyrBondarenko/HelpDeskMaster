using MediatR;

namespace HelpDeskMaster.App.UseCases.WorkRequest.WorkDirections.DeleteWorkDirection
{
    public class DeleteWorkDirectionCommand : IRequest<bool>
    {
        public required Guid WorkDirectionId { get; set; }
    }
}