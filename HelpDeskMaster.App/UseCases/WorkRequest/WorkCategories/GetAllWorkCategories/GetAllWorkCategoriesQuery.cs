using HelpDeskMaster.Domain.Entities.WorkCategories;
using MediatR;

namespace HelpDeskMaster.App.UseCases.WorkRequest.WorkCategories.GetAllWorkCategories
{
    public record class GetAllWorkCategoriesQuery : IRequest<List<WorkCategory>>
    {
    }
}