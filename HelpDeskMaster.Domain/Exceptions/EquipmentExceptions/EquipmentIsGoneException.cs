namespace HelpDeskMaster.Domain.Exceptions.EquipmentExceptions
{
    public class EquipmentIsGoneException : DomainException
    {
        public EquipmentIsGoneException(Guid equipmentId) 
            : base(DomainErrorCode.Gone, $"Equipment with id {equipmentId} is gone")
        {
        }
    }
}
