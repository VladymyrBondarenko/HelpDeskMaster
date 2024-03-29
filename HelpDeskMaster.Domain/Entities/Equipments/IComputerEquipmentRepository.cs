﻿namespace HelpDeskMaster.Domain.Entities.Equipments
{
    public interface IComputerEquipmentRepository
    {
        Task<bool> IsEquipmentAssignedAsync(Guid equipmentId, DateTimeOffset assignDate, CancellationToken cancellationToken);

        Task<bool> IsEquipmentComputerAsync(Guid equipmentId, CancellationToken cancellationToken);

        void Insert(ComputerEquipment computerEquipment);
    }
}