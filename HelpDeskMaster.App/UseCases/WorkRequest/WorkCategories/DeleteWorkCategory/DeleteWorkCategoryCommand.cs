using MediatR;

namespace HelpDeskMaster.App.UseCases.WorkRequest.WorkCategories.DeleteWorkCategory
{
    public record class DeleteWorkCategoryCommand(Guid WorkCategoryId) : IRequest
    {
    }
}
