using HelpDeskMaster.App.UseCases.Equipment.Equipments.CreateComputer;
using HelpDeskMaster.App.UseCases.Equipment.Equipments.CreateEquipment;
using HelpDeskMaster.App.UseCases.Equipment.Equipments.GetEquipmentById;
using HelpDeskMaster.Domain.Entities.EquipmentTypes;
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

        [HttpGet("equipment/{equipmentId}")]
        [ProducesResponseType(200, Type = typeof(GetEquipmentResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetEquipmentById(Guid equipmentId, CancellationToken cancellationToken)
        {
            var query = new GetEquipmentByIdQuery(equipmentId);

            var equipment = await _sender.Send(query, cancellationToken);

            var equipmentType = new EquipmentTypeModel(
                    equipment.EquipmentType?.Id ?? Guid.Empty,
                    equipment.EquipmentType?.Title ?? string.Empty,
                    equipment.EquipmentType?.TypeOfEquipment ?? TypeOfEquipment.Equipment,
                    equipment.EquipmentType?.CreatedAt ?? DateTimeOffset.MinValue,
                    equipment.EquipmentType?.UpdatedAt ?? DateTimeOffset.MinValue);

            var response = new GetEquipmentResponse(
                equipment.Id,
                equipment.EquipmentTypeId,
                equipmentType,
                equipment.Model,
                equipment.CommissioningDate,
                equipment.FactoryNumber,
                equipment.Price,
                equipment.CreatedAt,
                equipment.UpdatedAt);

            return Ok(new ResponseBody<GetEquipmentResponse>(response));
        }

        [HttpGet("computer/{computerId}")]
        [ProducesResponseType(200, Type = typeof(GetEquipmentResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetComputerById(Guid computerId, CancellationToken cancellationToken)
        {
            var query = new GetEquipmentByIdQuery(computerId);

            var computer = await _sender.Send(query, cancellationToken);

            var equipmentType = new EquipmentTypeModel(
                    computer.EquipmentType!.Id,
                    computer.EquipmentType!.Title,
                    computer.EquipmentType!.TypeOfEquipment,
                    computer.EquipmentType!.CreatedAt,
                    computer.EquipmentType!.UpdatedAt);

            var response = new GetComputerResponse(
                computer.Id,
                computer.EquipmentTypeId,
                equipmentType,
                computer.Model,
                computer.CommissioningDate,
                computer.FactoryNumber,
                computer.Price,
                computer.EquipmentComputerInfo!.Code,
                computer.EquipmentComputerInfo!.NameInNet,
                computer.EquipmentComputerInfo!.WarrantyMonths,
                computer.EquipmentComputerInfo!.InvoiceDate,
                computer.EquipmentComputerInfo!.WarrantyCardDate,
                computer.CreatedAt,
                computer.UpdatedAt);

            return Ok(new ResponseBody<GetComputerResponse>(response));
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

            var createdEquipment = await _sender.Send(cmd, cancellationToken);

            var response = new CreateEquipmentResponse(
                createdEquipment.Id,
                createdEquipment.EquipmentTypeId,
                createdEquipment.Model,
                createdEquipment.CommissioningDate,
                createdEquipment.FactoryNumber,
                createdEquipment.Price,
                createdEquipment.CreatedAt);

            return CreatedAtAction(nameof(CreateEquipment), new ResponseBody<CreateEquipmentResponse>(response));
        }

        [HttpPost("createComputer")]
        public async Task<IActionResult> CreateComputer([FromBody] CreateComputerRequest request,
            CancellationToken cancellationToken)
        {
            var cmd = new CreateComputerCommand(
                request.EquipmentTypeId,
                request.Model,
                request.CommissioningDate,
                request.FactoryNumber,
                request.Price,
                request.Code,
                request.NameInNet,
                request.WarrantyMonths,
                request.InvoiceDate,
                request.WarrantyCardDate);

            var createdComputer = await _sender.Send(cmd, cancellationToken);

            var response = new CreateComputerResponse(
                createdComputer.Id,
                createdComputer.EquipmentTypeId,
                createdComputer.Model,
                createdComputer.CommissioningDate,
                createdComputer.FactoryNumber,
                createdComputer.Price,
                createdComputer.EquipmentComputerInfo!.Code,
                createdComputer.EquipmentComputerInfo!.NameInNet,
                createdComputer.EquipmentComputerInfo!.WarrantyMonths,
                createdComputer.EquipmentComputerInfo!.InvoiceDate,
                createdComputer.CreatedAt);

            return CreatedAtAction(nameof(CreateComputer), new ResponseBody<CreateComputerResponse>(response));
        }
    }
}
