using HelpDeskMaster.App.UseCases.Equipment.EquipmentTypes.CreateEquipmentType;
using HelpDeskMaster.App.UseCases.Equipment.EquipmentTypes.GetAllEquipmentTypes;
using HelpDeskMaster.WebApi.Contracts;
using HelpDeskMaster.WebApi.Contracts.Equipment.Requests;
using HelpDeskMaster.WebApi.Contracts.Equipment.Responses;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDeskMaster.WebApi.Controllers
{
    [ApiController]
    [Route("api/equipmentTypes")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EquipmentTypeController : ControllerBase
    {
        private readonly ISender _sender;

        public EquipmentTypeController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(GetAllEquipmentTypesResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
        {
            var equipmentTypes = await _sender.Send(new GetAllEquipmentTypesQuery());

            var response = new GetAllEquipmentTypesResponse
            {
                EquipmentTypes = equipmentTypes.Select(x => new EquipmentTypeModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    TypeOfEquipment = x.TypeOfEquipment,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                }).ToList()
            };

            return Ok(new ResponseBody<GetAllEquipmentTypesResponse>(response));
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CreateEquipmentTypeResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create([FromBody] CreateEquipmentTypeRequest request,
            CancellationToken cancellationToken)
        {
            var cmd = new CreateEquipmentTypeCommand(request.Title, request.TypeOfEquipment);

            var equipmentType = await _sender.Send(cmd, cancellationToken);

            var response = new CreateEquipmentTypeResponse
            {
                Id = equipmentType.Id,
                Title = equipmentType.Title,
                TypeOfEquipment = equipmentType.TypeOfEquipment,
                CreatedAt = equipmentType.CreatedAt
            };

            return CreatedAtAction(nameof(Create), new ResponseBody<CreateEquipmentTypeResponse>(response));
        }
    }
}