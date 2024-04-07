using HelpDeskMaster.App.UseCases.User.GetUserByLogin;
using HelpDeskMaster.WebApi.Contracts;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HelpDeskMaster.WebApi.Contracts.User.Responses;

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

            if(user == null)
            {
                return NotFound();
            }

            var response = new GetUserByLoginResponse 
            { 
                Id = user.Id,
                Login = user.Login.Value,
                PhoneNumber = user.PhoneNumber,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };

            return Ok(new ResponseBody<GetUserByLoginResponse>(response));
        }
    }
}
