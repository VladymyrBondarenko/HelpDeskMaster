using Ardalis.GuardClauses;
using HelpDeskMaster.Domain.Abstractions;

namespace HelpDeskMaster.Domain.Entities.Equipments
{
    public class EquipmentComputerInfo : Entity
    {
        public EquipmentComputerInfo(Guid id,
            Guid computerId,
            string code,
            string nameInNet,
            int warrantyMonths,
            DateTimeOffset invoiceDate,
            DateTimeOffset warrantyCardDate,
            DateTimeOffset createdAt) : base(id, createdAt)
        {
            ComputerId = Guard.Against.Default(computerId);
            Code = Guard.Against.NullOrWhiteSpace(code);
            NameInNet = Guard.Against.NullOrWhiteSpace(nameInNet);
            WarrantyMonths = Guard.Against.Negative(warrantyMonths);
            InvoiceDate = invoiceDate;
            WarrantyCardDate = warrantyCardDate;
        }

        public Guid ComputerId { get; set; }

        public string Code { get; private set; }

        public string NameInNet { get; private set; }

        public int WarrantyMonths { get; private set; }

        public DateTimeOffset InvoiceDate { get; private set; }

        public DateTimeOffset WarrantyCardDate { get; private set; }
    }
}