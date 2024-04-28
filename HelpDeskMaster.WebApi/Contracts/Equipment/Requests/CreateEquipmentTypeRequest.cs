using HelpDeskMaster.Domain.Entities.EquipmentTypes;

namespace HelpDeskMaster.WebApi.Contracts.Equipment.Requests
{
    public class CreateEquipmentTypeRequest
    {
        public required string Title { get; set; }

        public required TypeOfEquipment TypeOfEquipment { get; set; }
    }
}
