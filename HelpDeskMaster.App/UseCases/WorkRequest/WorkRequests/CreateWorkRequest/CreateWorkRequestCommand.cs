using MediatR;

namespace HelpDeskMaster.App.UseCases.WorkRequest.WorkRequests.CreateWorkRequest
{
    public record class CreateWorkRequestCommand(
        Guid AuthorId,
        Guid WorkDirectionId,
        Guid WorkCategoryId,
        DateTimeOffset FailureRevealedDate,
        DateTimeOffset DesiredExecutionDate,
        string? Content) : IRequest<Domain.Entities.WorkRequests.WorkRequest>
    {
    }
}
