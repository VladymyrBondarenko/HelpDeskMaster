using MediatR;

namespace HelpDeskMaster.App.UseCases.WorkRequest.WorkCategories.CreateWorkCategory
{
    public class CreateWorkCategoryCommand : IRequest<Guid>
    {
        public required string Title { get; set; }
    }
}