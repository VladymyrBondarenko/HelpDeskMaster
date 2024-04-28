using MediatR;

namespace HelpDeskMaster.App.UseCases.Equipment.Equipments.GetEquipmentById
{
    public record GetEquipmentByIdQuery(Guid Id) : IRequest<Domain.Entities.Equipments.Equipment>
    {
    }
}
