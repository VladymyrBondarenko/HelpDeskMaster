using HelpDeskMaster.Domain.Entities.EquipmentTypes;

namespace HelpDeskMaster.WebApi.Contracts.Equipment.Responses
{
    public class CreateEquipmentResponse
    {
        public required Guid Id { get; set; }

        public required Guid EquipmentTypeId { get; set; }

        public EquipmentType? EquipmentType { get; set; }

        public string? Model { get; set; }

        public required DateTimeOffset CommissioningDate { get; set; }

        public string? FactoryNumber { get; set; }

        public required decimal Price { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
