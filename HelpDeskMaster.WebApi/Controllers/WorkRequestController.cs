using HelpDeskMaster.App.UseCases.WorkRequest.WorkRequests.CreateWorkRequest;
using HelpDeskMaster.App.UseCases.WorkRequest.WorkRequests.GetWorkRequestById;
using HelpDeskMaster.WebApi.Contracts;
using HelpDeskMaster.WebApi.Contracts.WorkRequest.Requests;
using HelpDeskMaster.WebApi.Contracts.WorkRequest.Responses;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDeskMaster.WebApi.Controllers
{
    [ApiController]
    [Route("api/workRequests")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WorkRequestController : ControllerBase
    {
        private readonly ISender _sender;

        public WorkRequestController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{workRequestId}")]
        [ProducesResponseType(200, Type = typeof(GetWorkRequestResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetWorkRequestById(
            Guid workRequestId, 
            CancellationToken cancellationToken)
        {
            var query = new GetWorkRequestByIdQuery(workRequestId);

            var workRequest = await _sender.Send(query, cancellationToken);

            var response = new GetWorkRequestResponse(
                workRequest.Id,
                workRequest.AuthorId,
                workRequest.WorkDirectionId,
                new WorkDirectionModel(
                    workRequest.WorkDirection!.Id, 
                    workRequest.WorkDirection!.Title,
                    workRequest.WorkDirection!.CreatedAt, 
                    workRequest.WorkDirection!.UpdatedAt),
                workRequest.WorkCategoryId,
                new WorkCategoryModel(
                    workRequest.WorkCategory!.Id,
                    workRequest.WorkCategory!.Title,
                    workRequest.WorkCategory!.CreatedAt,
                    workRequest.WorkCategory!.UpdatedAt),
                workRequest.FailureRevealedDate,
                workRequest.DesiredExecutionDate,
                workRequest.Content,
                workRequest.CreatedAt);

            return Ok(new ResponseBody<GetWorkRequestResponse>(response));
        }

        [HttpPost()]
        [ProducesResponseType(201, Type = typeof(CreateWorkRequestResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create(
            [FromBody] CreateWorkRequestRequest request, 
            CancellationToken cancellation)
        {
            var cmd = new CreateWorkRequestCommand(
                request.AuthorId,
                request.WorkDirectionId,
                request.WorkCategoryId,
                request.FailureRevealedDate,
                request.DesiredExecutionDate,
                request.Content);

            var createdWorkRequest = await _sender.Send(cmd, cancellation);

            var response = new CreateWorkRequestResponse(
                createdWorkRequest.Id,
                createdWorkRequest.AuthorId,
                createdWorkRequest.WorkDirectionId,
                createdWorkRequest.WorkCategoryId,
                createdWorkRequest.FailureRevealedDate,
                createdWorkRequest.DesiredExecutionDate,
                createdWorkRequest.Content,
                createdWorkRequest.CreatedAt);

            return CreatedAtAction(nameof(Create), new ResponseBody<CreateWorkRequestResponse>(response));
        }
    }
}