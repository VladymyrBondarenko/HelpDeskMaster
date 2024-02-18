using MediatR;

namespace HelpDeskMaster.App.UseCases.WorkRequest.WorkDirections.CreateWorkDirection
{
    public class CreateWorkDirectionCommand : IRequest<Guid>
    {
        public required string Title { get; set; }
    }
}
