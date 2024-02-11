namespace HelpDeskMaster.Domain.Exceptions.EquipmentExceptions
{
    public class AssignComputerToItselfException : DomainException
    {
        public AssignComputerToItselfException(Guid equipmentId)
            : base(DomainErrorCode.BadRequest, $"Computer with id {equipmentId} cannot assign it to itself")
        {
        }
    }
}