using MediatR;

namespace HelpDeskMaster.App.UseCases.WorkRequest.WorkRequests.GetWorkRequestById
{
    public record GetWorkRequestByIdQuery(Guid Id) 
        : IRequest<Domain.Entities.WorkRequests.WorkRequest>
    {
    }
}
