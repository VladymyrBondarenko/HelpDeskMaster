using Ardalis.GuardClauses;
using HelpDeskMaster.Domain.Abstractions;

namespace HelpDeskMaster.Domain.Entities.EquipmentTypes
{
    public class EquipmentType : Entity
    {
        internal EquipmentType(Guid id, string title, DateTimeOffset createdAt,
            TypeOfEquipment typeOfEquipment) : base(id, createdAt)
        {
            Title = Guard.Against.NullOrWhiteSpace(title);
            TypeOfEquipment = Guard.Against.Null(typeOfEquipment);
        }

        public string Title { get; private set; }

        public TypeOfEquipment TypeOfEquipment { get; private set; }
    }
}