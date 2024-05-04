﻿namespace HelpDeskMaster.WebApi.Contracts.Equipment.Responses
{
    public record GetEquipmentResponse(
        Guid Id,
        Guid EquipmentTypeId,
        EquipmentTypeModel? EquipmentType,
        string? Model,
        DateTimeOffset CommissioningDate,
        string? FactoryNumber,
        decimal Price,
        DateTimeOffset CreatedAt,
        DateTimeOffset? UpdatedAt)
    {
    }
}