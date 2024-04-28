using HelpDeskMaster.App.UseCases.Equipment.Equipments.CreateEquipment;
using HelpDeskMaster.App.UseCases.Equipment.Equipments.GetEquipmentById;
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
    [Route("api/equipments")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EquipmentController : ControllerBase
    {
        private readonly ISender _sender;

        public EquipmentController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{equipmentId}")]
        [ProducesResponseType(200, Type = typeof(GetEquipmentResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetEquipmentById(Guid equipmentId, CancellationToken cancellationToken)
        {
            var query = new GetEquipmentByIdQuery(equipmentId);

            var equipment = await _sender.Send(query, cancellationToken);

            var response = new GetEquipmentResponse
            {
                Id = equipment.Id,
                EquipmentTypeId = equipment.EquipmentTypeId,
                EquipmentType = equipment.EquipmentType,
                CommissioningDate = equipment.CommissioningDate,
                FactoryNumber = equipment.FactoryNumber,
                Model = equipment.Model,
                Price = equipment.Price,
                CreatedAt = equipment.CreatedAt,
                UpdatedAt = equipment.UpdatedAt
            };

            return Ok(new ResponseBody<GetEquipmentResponse>(response));
        }

        [HttpPost("createEquipment")]
        [ProducesResponseType(201, Type = typeof(CreateEquipmentResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateEquipment([FromBody] CreateEquipmentRequest request,
            CancellationToken cancellationToken)
        {
            var cmd = new CreateEquipmentCommand(
                request.EquipmentTypeId,
                request.Model,
                request.CommissioningDate,
                request.FactoryNumber,
                request.Price);

            var createEquipment = await _sender.Send(cmd, cancellationToken);

            var response = new CreateEquipmentResponse 
            { 
                Id = createEquipment.Id,
                EquipmentTypeId = createEquipment.EquipmentTypeId,
                EquipmentType = createEquipment.EquipmentType,
                CommissioningDate = createEquipment.CommissioningDate,
                FactoryNumber = createEquipment.FactoryNumber,
                Model = createEquipment.Model,
                Price = createEquipment.Price,
                CreatedAt = createEquipment.CreatedAt
            };

            return CreatedAtAction(nameof(CreateEquipment), new ResponseBody<CreateEquipmentResponse>(response));
        }
    }
}
