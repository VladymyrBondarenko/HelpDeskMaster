using Ardalis.GuardClauses;
using HelpDeskMaster.Domain.Abstractions;
using HelpDeskMaster.Domain.Entities.EquipmentTypes;

namespace HelpDeskMaster.Domain.Entities.Equipments
{
    public class Equipment : Entity
    {
        internal Equipment(Guid id, 
            Guid equipmentTypeId, 
            string? model,
            DateTimeOffset commissioningDate, 
            string? factoryNumber, 
            decimal price,
            DateTimeOffset createdAt) : base(id, createdAt)
        {
            EquipmentTypeId = Guard.Against.Default(equipmentTypeId);
            Model = model;
            CommissioningDate = Guard.Against.Default(commissioningDate);
            FactoryNumber = factoryNumber;
            Price = Guard.Against.Negative(price);
        }

        public Guid EquipmentTypeId { get; private set; }

        public EquipmentType? EquipmentType { get; }

        public string? Model { get; private set; }

        public DateTimeOffset CommissioningDate { get; private set; }

        public string? FactoryNumber { get; private set; }

        public decimal Price { get; private set; }

        public EquipmentComputerInfo? EquipmentComputerInfo { get; private set; }

        public IReadOnlyList<ComputerEquipment> ComputerEquipments => _computerEquipments.ToList();

        private readonly List<ComputerEquipment> _computerEquipments = new();

        internal void AddComputerInfo(EquipmentComputerInfo computerInfo)
        {
            EquipmentComputerInfo = computerInfo;
        }

        internal ComputerEquipment AssignEquipmentToComputer(Guid equipmentId,
            DateTimeOffset assignDate)
        {
            var computerEquipment = new ComputerEquipment(Guid.NewGuid(),
                DateTimeOffset.UtcNow, Id, equipmentId, assignDate);

            _computerEquipments.Add(computerEquipment);

            return computerEquipment;
        }
    }
}