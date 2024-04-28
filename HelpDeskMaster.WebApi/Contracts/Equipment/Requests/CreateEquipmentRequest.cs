namespace HelpDeskMaster.WebApi.Contracts.Equipment.Requests
{
    public class CreateEquipmentRequest
    {
        public required Guid EquipmentTypeId {  get; set; }

        public string? Model {  get; set; }

        public required DateTimeOffset CommissioningDate { get; set; }

        public string? FactoryNumber { get; set; }

        public required decimal Price { get; set; }
    }
}
