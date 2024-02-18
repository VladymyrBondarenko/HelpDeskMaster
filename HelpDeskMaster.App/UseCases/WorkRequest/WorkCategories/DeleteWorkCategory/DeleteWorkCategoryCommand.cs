using MediatR;

namespace HelpDeskMaster.App.UseCases.WorkRequest.WorkCategories.DeleteWorkCategory
{
    public class DeleteWorkCategoryCommand : IRequest<bool>
    {
        public required Guid WorkCategoryId { get; set; }
    }
}
