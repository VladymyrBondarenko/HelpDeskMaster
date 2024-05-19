using HelpDeskMaster.Domain.Entities.Equipments;
using HelpDeskMaster.Domain.Entities.EquipmentTypes;
using HelpDeskMaster.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HelpDeskMaster.App.DataMocking
{
    internal class SimpleHdmDataMockService : IHdmDataMockService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<SimpleHdmDataMockService> _logger;

        public SimpleHdmDataMockService(
            ApplicationDbContext dbContext,
            ILogger<SimpleHdmDataMockService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task MockData()
        {
            _logger.LogInformation("Beginning mocking data");

            if (await _dbContext.EquipmentTypes.AnyAsync()) 
            {   
                _logger.LogInformation("Data already exists. Aborting data mocking");
                return;
            }

            #region Mock equipment types
            var pcPartsEquipmentType = new EquipmentType(
                Guid.NewGuid(),
                "PC Parts",
                TypeOfEquipment.Equipment,
                DateTimeOffset.UtcNow);
            var computerEquipmentType = new EquipmentType(
                Guid.NewGuid(),
                "Computer",
                TypeOfEquipment.PC,
                DateTimeOffset.UtcNow);
            var laptopEquipmentType = new EquipmentType(
                Guid.NewGuid(),
                "Laptop",
                TypeOfEquipment.PC,
                DateTimeOffset.UtcNow);
            await _dbContext.EquipmentTypes.AddRangeAsync(
            [
                pcPartsEquipmentType,
                computerEquipmentType,
                laptopEquipmentType
            ]);
            #endregion

            #region Mock equipments
            var ssdEquipment = new Equipment(
                Guid.NewGuid(),
                pcPartsEquipmentType.Id,
                "SSD Samsung 500GB",
                DateTimeOffset.UtcNow,
                "00023432949",
                1777.21m,
                DateTimeOffset.UtcNow);
            var graphicCardEquipment = new Equipment(
                Guid.NewGuid(),
                pcPartsEquipmentType.Id,
                "NVIDIA GeForce GTX 1060 6GB",
                DateTimeOffset.UtcNow,
                "0005435443",
                1777.21m,
                DateTimeOffset.UtcNow);
            var processorEquipment = new Equipment(
                Guid.NewGuid(),
                pcPartsEquipmentType.Id,
                "AMD Ryzen 5 2600 Six-Core",
                DateTimeOffset.UtcNow,
                "0006634234",
                1777.21m,
                DateTimeOffset.UtcNow);
            await _dbContext.Equipments.AddRangeAsync(
            [
                ssdEquipment,
                graphicCardEquipment, 
                processorEquipment
            ]);

            var computer = new Equipment(
                Guid.NewGuid(),
                computerEquipmentType.Id,
                "ARTLINE Computer",
                DateTimeOffset.UtcNow,
                "00066652343",
                3434.23m,
                DateTimeOffset.UtcNow);
            var laptop = new Equipment(
                Guid.NewGuid(),
                laptopEquipmentType.Id,
                "Acer Nitro 5 AN517-55-52XR (NH.QLFEU.00F) Obsidian Black",
                DateTimeOffset.UtcNow,
                "0005234134",
                13332.23m,
                DateTimeOffset.UtcNow);
            await _dbContext.Equipments.AddRangeAsync(
            [
                computer,
                laptop
            ]);

            var computerInfo = new EquipmentComputerInfo(
                Guid.NewGuid(),
                computer.Id,
                "165",
                "SB165",
                3,
                DateTimeOffset.UtcNow,
                DateTimeOffset.UtcNow,
                DateTimeOffset.UtcNow);
            var laptopInfo = new EquipmentComputerInfo(
                Guid.NewGuid(),
                laptop.Id,
                "244",
                "SB244",
                3,
                DateTimeOffset.UtcNow,
                DateTimeOffset.UtcNow,
                DateTimeOffset.UtcNow);
            await _dbContext.EquipmentComputerInfos.AddRangeAsync(
            [
                computerInfo,
                laptopInfo
            ]);
            #endregion

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Data mocking was completed successfully");
        }
    }
}
