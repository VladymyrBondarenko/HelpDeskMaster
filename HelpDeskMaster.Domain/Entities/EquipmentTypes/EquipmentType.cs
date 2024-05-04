using Ardalis.GuardClauses;
using HelpDeskMaster.Domain.Abstractions;

namespace HelpDeskMaster.Domain.Entities.EquipmentTypes
{
    public class EquipmentType : Entity
    {
        public EquipmentType(Guid id, string title, TypeOfEquipment typeOfEquipment,
            DateTimeOffset createdAt) : base(id, createdAt)
        {
            Title = Guard.Against.NullOrWhiteSpace(title);
            TypeOfEquipment = Guard.Against.Null(typeOfEquipment);
        }

        public string Title { get; private set; }

        public TypeOfEquipment TypeOfEquipment { get; private set; }
    }
}