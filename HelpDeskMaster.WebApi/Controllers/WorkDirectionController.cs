using HelpDeskMaster.App.UseCases.WorkRequest.WorkDirections.CreateWorkDirection;
using HelpDeskMaster.App.UseCases.WorkRequest.WorkDirections.DeleteWorkDirection;
using HelpDeskMaster.App.UseCases.WorkRequest.WorkDirections.GetAllWorkDirections;
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
    [Route("api/workDirections")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WorkDirectionController : ControllerBase
    {
        private readonly ISender _sender;

        public WorkDirectionController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost()]
        [ProducesResponseType(201, Type = typeof(CreateWorkDirectionResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create([FromBody] CreateWorkDirectionRequest createWorkDirectionRequest,
            CancellationToken cancellationToken)
        {
            var cmd = new CreateWorkDirectionCommand(createWorkDirectionRequest.Title);

            var workDirection = await _sender.Send(cmd, cancellationToken);

            var response = new CreateWorkDirectionResponse(
                workDirection.Id,
                workDirection.Title,
                workDirection.CreatedAt);

            return CreatedAtAction(nameof(Create), new ResponseBody<CreateWorkDirectionResponse>(response));
        }

        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(GetAllWorkDirectionsResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetAllWorkDirectionsQuery();

            var workDirections = await _sender.Send(query, cancellationToken);

            var response = new GetAllWorkDirectionsResponse(
                workDirections.Select(x => new WorkDirectionModel(
                    x.Id,
                    x.Title,
                    x.CreatedAt,
                    x.UpdatedAt)
                ).ToList());

            return Ok(new ResponseBody<GetAllWorkDirectionsResponse>(response));
        }

        [HttpDelete("{workDirectionId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(Guid workDirectionId, CancellationToken cancellationToken)
        {
            var cmd = new DeleteWorkDirectionCommand(workDirectionId);

            await _sender.Send(cmd, cancellationToken);

            return NoContent();
        }
    }
}