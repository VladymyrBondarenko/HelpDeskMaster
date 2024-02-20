using HelpDeskMaster.WebApi.Contracts.WorkRequest.Requests;
using HelpDeskMaster.WebApi.Contracts.WorkRequest.Responses;
using HelpDeskMaster.WebApi.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using HelpDeskMaster.App.UseCases.WorkRequest.WorkCategories.CreateWorkCategory;
using HelpDeskMaster.App.UseCases.WorkRequest.WorkCategories.GetAllWorkCategories;
using HelpDeskMaster.App.UseCases.WorkRequest.WorkCategories.DeleteWorkCategory;

namespace HelpDeskMaster.WebApi.Controllers
{
    [ApiController]
    [Route("api/WorkCategories")]
    public class WorkCategoryController : ControllerBase
    {
        private readonly ISender _sender;

        public WorkCategoryController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost()]
        [ProducesResponseType(201, Type = typeof(CreateWorkCategoryResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create([FromBody] CreateWorkCategoryRequest createWorkCategoryRequest,
            CancellationToken cancellationToken)
        {
            var cmd = new CreateWorkCategoryCommand(createWorkCategoryRequest.Title);

            var workCategory = await _sender.Send(cmd, cancellationToken);

            var response = new CreateWorkCategoryResponse
            {
                Id = workCategory.Id,
                Title = workCategory.Title,
                CreatedAt = workCategory.CreatedAt
            };

            return CreatedAtAction(nameof(Create), new ResponseBody<CreateWorkCategoryResponse>(response));
        }

        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(GetAllWorkCategoriesResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetAllWorkCategoriesQuery();

            var workCategories = await _sender.Send(query, cancellationToken);

            var response = new GetAllWorkCategoriesResponse
            {
                WorkCategories = workCategories.Select(x => new WorkCategoryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                }).ToList()
            };

            return Ok(new ResponseBody<GetAllWorkCategoriesResponse>(response));
        }

        [HttpDelete()]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete([FromQuery] Guid workCategoryId, CancellationToken cancellationToken)
        {
            var cmd = new DeleteWorkCategoryCommand(workCategoryId);

            await _sender.Send(cmd, cancellationToken);

            return NoContent();
        }
    }
}
