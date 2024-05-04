using HelpDeskMaster.App.UseCases.User.GetUserByLogin;
using HelpDeskMaster.WebApi.Contracts;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HelpDeskMaster.WebApi.Contracts.User.Responses;
using HelpDeskMaster.App.UseCases.User.AssignEquipmentToUser;
using HelpDeskMaster.WebApi.Contracts.User.Request;

namespace HelpDeskMaster.WebApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;

        public UserController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{login}")]
        [ProducesResponseType(200, Type = typeof(GetUserByLoginResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUserByLogin(string login, CancellationToken cancellationToken)
        {
            var query = new GetUserByLoginQuery(login);

            var user = await _sender.Send(query, cancellationToken);

            var response = new GetUserByLoginResponse(
                user.Id, 
                user.Login.Value, 
                user.PhoneNumber, 
                user.CreatedAt, 
                user.UpdatedAt);

            return Ok(new ResponseBody<GetUserByLoginResponse>(response));
        }

        [HttpPost("assignEquipment")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task <IActionResult> AssignEquipmentToUser([FromBody] AssignEquipmentToUserRequest request,
            CancellationToken cancellationToken)
        {
            var cmd = new AssignEquipmentToUserCommand(
                request.UserId,
                request.EquipmentId,
                request.AssignDate);

            await _sender.Send(cmd, cancellationToken);

            return Created();
        }
    }
}
