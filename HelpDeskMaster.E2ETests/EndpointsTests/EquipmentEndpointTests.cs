using FluentAssertions;
using HelpDeskMaster.Domain.Entities.Equipments;
using HelpDeskMaster.Domain.Entities.EquipmentTypes;
using HelpDeskMaster.Persistence.Data;
using HelpDeskMaster.WebApi.Contracts;
using HelpDeskMaster.WebApi.Contracts.Equipment.Requests;
using HelpDeskMaster.WebApi.Contracts.Equipment.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Xunit;

namespace HelpDeskMaster.E2ETests.EndpointsTests
{
    public class EquipmentEndpointTests : HdmEndpointTestBase
    {
        private readonly HdmServerApplicationFactory _factory;

        public EquipmentEndpointTests(HdmServerApplicationFactory factory) : base(factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ShouldReturnEquipmentById()
        {
            await AuthenticateAsync();

            await using var scope = _factory.Services.CreateAsyncScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var date = new DateTimeOffset(
                2024, 4, 28,
                12, 34, 5,
                new TimeSpan());

            // create equipment type
            var equipmentType = new EquipmentType(
                new Guid("8812e395-9194-403a-8d19-cf60fb695d68"),
                "PC Parts",
                TypeOfEquipment.Equipment,
                date);
            await db.EquipmentTypes.AddAsync(equipmentType);

            // create equipment
            var equipment = new Equipment(
                new Guid("ef084248-026e-4748-ac54-19cbc8afc70c"),
                equipmentType.Id,
                "SSD Samsung 500GB",
                date,
                "00023432949",
                1777.21m,
                date);
            await db.Equipments.AddAsync(equipment);

            // save simulated data to db
            await db.SaveChangesAsync();

            // call for get equipment by id endpoint
            using var response = await HttpClient.GetAsync($"api/equipments/equipment/{equipment.Id}");
            response.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            var reponseBody = await response.Content.ReadFromJsonAsync<ResponseBody<GetEquipmentResponse>>();

            // assert response body
            reponseBody.Should().NotBeNull()
                    .And.Subject.As<ResponseBody<GetEquipmentResponse>>()
                .Data.Should().NotBeNull();

            reponseBody!.Data!.Id.Should().Be(equipment.Id);
            reponseBody!.Data!.EquipmentTypeId.Should().Be(equipment.EquipmentTypeId);
            reponseBody!.Data!.Model.Should().Be(equipment.Model);
            reponseBody!.Data!.CommissioningDate.Should().Be(equipment.CommissioningDate);
            reponseBody!.Data!.FactoryNumber.Should().Be(equipment.FactoryNumber);
            reponseBody!.Data!.Price.Should().Be(equipment.Price);
            reponseBody!.Data!.CreatedAt.Should().Be(equipment.CreatedAt);
            reponseBody!.Data!.UpdatedAt.Should().Be(equipment.UpdatedAt);

            reponseBody!.Data!.EquipmentType.Should().NotBeNull();
            reponseBody!.Data!.EquipmentType!.Id.Should().Be(equipmentType.Id);
            reponseBody!.Data!.EquipmentType!.Title.Should().Be(equipmentType.Title);
            reponseBody!.Data!.EquipmentType!.TypeOfEquipment.Should().Be(equipmentType.TypeOfEquipment);
            reponseBody!.Data!.EquipmentType!.CreatedAt.Should().Be(equipmentType.CreatedAt);
        }

        [Fact]
        public async Task ShouldCreateEquipment()
        {
            await AuthenticateAsync();

            await using var scope = _factory.Services.CreateAsyncScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var date = new DateTimeOffset(
                2024, 4, 28,
                12, 34, 5,
                new TimeSpan());

            // create equipmentType
            var equipmentType = new EquipmentType(
                new Guid("2bd6b706-3159-4f80-bf42-6f13727238eb"), 
                "PC Parts", 
                TypeOfEquipment.Equipment,
                date);
            await db.EquipmentTypes.AddAsync(equipmentType, CancellationToken.None);
            await db.SaveChangesAsync();

            // call for create equipment endpoint
            var request = new CreateEquipmentRequest(
                equipmentType.Id,
                "SSD Kingstone 240GB",
                date,
                "123000232132",
                1321.12m);

            using var response = await HttpClient.PostAsJsonAsync("api/equipments/createEquipment",
                request, CancellationToken.None);
            response.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            var reponseBody = await response.Content.ReadFromJsonAsync<ResponseBody<CreateEquipmentResponse>>();

            // validate response body
            reponseBody.Should().NotBeNull()
                    .And.Subject.As<ResponseBody<CreateEquipmentResponse>>()
                .Data.Should().NotBeNull()
                    .And.Subject.As<CreateEquipmentResponse>().Id.Should().NotBeEmpty();
            reponseBody!.Data.EquipmentTypeId.Should().Be(equipmentType.Id);

            // check equipment in db
            var equipmentInDb = await db.Equipments
                .Include(x => x.EquipmentType)
                .SingleOrDefaultAsync(x => x.Id == reponseBody!.Data.Id);

            equipmentInDb.Should().NotBeNull();
            equipmentInDb!.EquipmentType
                .Should().NotBeNull()
                    .And.Subject.As<EquipmentType>()
                .Id.Should().Be(equipmentType.Id);
        }

        [Fact]
        public async Task ShouldReturnComputerById()
        {
            await AuthenticateAsync();

            await using var scope = _factory.Services.CreateAsyncScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var date = new DateTimeOffset(
                2024, 4, 28,
                12, 34, 5,
                new TimeSpan());

            // create equipment type
            var equipmentType = new EquipmentType(
                new Guid("1a61f403-8a47-465f-b51a-eab392574252"),
                "Computer",
                TypeOfEquipment.PC,
                date);
            await db.EquipmentTypes.AddAsync(equipmentType);

            // create computer
            var computer = new Equipment(
                new Guid("7de0ebce-2237-43b7-a4ff-a623ebcd328f"),
                equipmentType.Id,
                "ARTLINE Computer",
                date,
                "00023432949",
                1777.21m,
                date);
            await db.Equipments.AddAsync(computer);

            var computerInfo = new EquipmentComputerInfo(
                new Guid("aa270b8a-ab07-4606-aa44-19f3a6416c9f"),
                computer.Id,
                "165",
                "SB165",
                3,
                date,
                date,
                date);
            await db.EquipmentComputerInfos.AddAsync(computerInfo);

            // save simulated data in db
            await db.SaveChangesAsync();

            // call for get computer by id endpoint
            using var response = await HttpClient.GetAsync($"api/equipments/computer/{computer.Id}");
            response.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            var reponseBody = await response.Content.ReadFromJsonAsync<ResponseBody<GetComputerResponse>>();

            // validate response body
            reponseBody.Should().NotBeNull()
                    .And.Subject.As<ResponseBody<GetComputerResponse>>()
                .Data.Should().NotBeNull();

            reponseBody!.Data!.Id.Should().Be(computer.Id);
            reponseBody!.Data!.EquipmentTypeId.Should().Be(computer.EquipmentTypeId);
            reponseBody!.Data!.Model.Should().Be(computer.Model);
            reponseBody!.Data!.CommissioningDate.Should().Be(computer.CommissioningDate);
            reponseBody!.Data!.FactoryNumber.Should().Be(computer.FactoryNumber);
            reponseBody!.Data!.Price.Should().Be(computer.Price);
            reponseBody!.Data!.Code.Should().Be(computerInfo.Code);
            reponseBody!.Data!.NameInNet.Should().Be(computerInfo.NameInNet);
            reponseBody!.Data!.WarrantyMonths.Should().Be(computerInfo.WarrantyMonths);
            reponseBody!.Data!.InvoiceDate.Should().Be(computerInfo.InvoiceDate);
            reponseBody!.Data!.WarrantyCardDate.Should().Be(computerInfo.WarrantyCardDate);
            reponseBody!.Data!.CreatedAt.Should().Be(computerInfo.CreatedAt);
            reponseBody!.Data!.UpdatedAt.Should().Be(computerInfo.UpdatedAt);

            reponseBody!.Data!.EquipmentType.Should().NotBeNull();
            reponseBody!.Data!.EquipmentType!.Id.Should().Be(equipmentType.Id);
            reponseBody!.Data!.EquipmentType!.Title.Should().Be(equipmentType.Title);
            reponseBody!.Data!.EquipmentType!.TypeOfEquipment.Should().Be(equipmentType.TypeOfEquipment);
            reponseBody!.Data!.EquipmentType!.CreatedAt.Should().Be(equipmentType.CreatedAt);
        }

        [Fact]
        public async Task ShouldCreateComputer()
        {
            await AuthenticateAsync();

            await using var scope = _factory.Services.CreateAsyncScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var date = new DateTimeOffset(
                2024, 4, 28,
                12, 34, 5,
                new TimeSpan());

            // create equipment type
            var equipmentType = new EquipmentType(
                new Guid("f0275515-f253-40b3-824e-b1101f82e321"),
                "Computer",
                TypeOfEquipment.PC,
                date);
            await db.EquipmentTypes.AddAsync(equipmentType, CancellationToken.None);
            await db.SaveChangesAsync();

            // call for create computer endpoint
            var request = new CreateComputerRequest(
                equipmentType.Id,
                "ARTLINE Computer",
                date,
                "123000232132",
                1321.12m,
                "165",
                "SB165",
                3,
                date,
                date);

            using var response = await HttpClient.PostAsJsonAsync("api/equipments/createComputer",
                request, CancellationToken.None);
            response.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            var reponseBody = await response.Content.ReadFromJsonAsync<ResponseBody<CreateComputerResponse>>();

            // validate response body
            reponseBody.Should().NotBeNull()
                    .And.Subject.As<ResponseBody<CreateComputerResponse>>()
                .Data.Should().NotBeNull()
                    .And.Subject.As<CreateComputerResponse>().Id.Should().NotBeEmpty();
            reponseBody!.Data.EquipmentTypeId.Should().Be(equipmentType.Id);

            // check for computer in db
            var computerInDb = await db.Equipments.SingleOrDefaultAsync(x => x.Id == reponseBody!.Data.Id);
            computerInDb.Should().NotBeNull();

            computerInDb.Should().NotBeNull();
            computerInDb!.EquipmentType
                .Should().NotBeNull()
                    .And.Subject.As<EquipmentType>()
                .Id.Should().Be(equipmentType.Id);
        }

        [Fact]
        public async Task ShouldAssignEquipmentToComputer()
        {
            await AuthenticateAsync();

            await using var scope = _factory.Services.CreateAsyncScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var date = new DateTimeOffset(
                2024, 1, 24,
                12, 34, 5,
                new TimeSpan());

            // create equipment types
            var partsEquipmentType = new EquipmentType(
                new Guid("b54a1600-563d-4190-bd60-5d660638d1e0"),
                "PC Parts",
                TypeOfEquipment.Equipment,
                date);

            var computerEquipmentType = new EquipmentType(
                new Guid("a0d25cc5-4c37-4584-a5ad-c46a67923690"),
                "Computer",
                TypeOfEquipment.PC,
                date);

            await db.EquipmentTypes.AddRangeAsync(partsEquipmentType, computerEquipmentType);

            // create equipement and computer
            var equipment = new Equipment(
                new Guid("e5d2f073-6e65-4156-bf57-73939d72a288"),
                partsEquipmentType.Id,
                "SSD Samsung 500GB",
                date,
                "00023432949",
                1777.21m,
                date);
            await db.Equipments.AddAsync(equipment);

            var computer = new Equipment(
                new Guid("027fc702-7f1b-4414-90f9-d81b50aed367"),
                computerEquipmentType.Id,
                "ARTLINE Computer",
                date,
                "00023432949",
                1777.21m,
                date);
            await db.Equipments.AddAsync(computer);

            var computerInfo = new EquipmentComputerInfo(
                new Guid("b7581696-18ff-42c4-a91d-48d530b05680"),
                computer.Id,
                "165",
                "SB165",
                3,
                date,
                date,
                date);
            await db.EquipmentComputerInfos.AddAsync(computerInfo);

            // save simulated data to db
            await db.SaveChangesAsync();

            // call assign equipment to computer endpoint
            var assignDate = new DateTimeOffset(
                2024, 5, 4,
                19, 19, 5,
                new TimeSpan());

            var request = new AssignEquipmentToComputerRequest(
                computer.Id,
                equipment.Id,
                assignDate);

            using var response = await HttpClient.PostAsJsonAsync("api/equipments/assignToComputer",
                request, CancellationToken.None);
            response.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            // check for computer equipment in db
            var computerEquipmentInDb = await db.ComputerEquipments
                .SingleOrDefaultAsync(x => x.ComputerId == computer.Id);
            computerEquipmentInDb.Should().NotBeNull();
            computerEquipmentInDb!.AssignedDate.Should().Be(assignDate);
            computerEquipmentInDb!.EquipmentId.Should().Be(equipment.Id);
        }
    }
}
