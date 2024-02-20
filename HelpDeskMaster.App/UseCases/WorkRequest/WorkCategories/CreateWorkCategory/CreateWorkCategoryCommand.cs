using HelpDeskMaster.Domain.Entities.WorkCategories;
using MediatR;

namespace HelpDeskMaster.App.UseCases.WorkRequest.WorkCategories.CreateWorkCategory
{
    public record class CreateWorkCategoryCommand(string Title) : IRequest<WorkCategory>
    {
    }
}