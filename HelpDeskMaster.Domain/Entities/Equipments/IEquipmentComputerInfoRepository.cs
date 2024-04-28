namespace HelpDeskMaster.Domain.Entities.Equipments
{
    public interface IEquipmentComputerInfoRepository
    {
        Task InsertAsync(EquipmentComputerInfo computerInfo, CancellationToken cancellationToken);
    }
}
