using HelpDeskMaster.Domain.Entities.EquipmentTypes;

namespace HelpDeskMaster.WebApi.Contracts.Equipment.Responses
{
    public class EquipmentTypeModel
    {
        public required Guid Id { get; set; }

        public required string Title { get; set; }

        public required TypeOfEquipment TypeOfEquipment { get; set; }

        public required DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
