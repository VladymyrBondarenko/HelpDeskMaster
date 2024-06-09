using FluentAssertions;
using HelpDeskMaster.Domain.Entities.Equipments;
using HelpDeskMaster.Domain.Entities.EquipmentTypes;
using HelpDeskMaster.Domain.Entities.Users;
using HelpDeskMaster.Persistence.Data;
using HelpDeskMaster.WebApi.Contracts;
using HelpDeskMaster.WebApi.Contracts.User.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Xunit;
using HelpDeskMaster.WebApi.Contracts.User.Responses;
using Newtonsoft.Json;
using HelpDeskMaster.Domain.DomainEvents;
using HelpDeskMaster.E2ETests.Probing;
using HelpDeskMaster.E2ETests.BackgroundJobsTests.ProcessOutboxMessagesJob;

namespace HelpDeskMaster.E2ETests.EndpointsTests
{
    [Collection(nameof(ProbingTestsCollectionDefinition))]
    public class UserEndpointTests : HdmEndpointTestBase
    {
        private readonly HdmServerApplicationFactory _factory;

        public UserEndpointTests(HdmServerApplicationFactory factory) : base(factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ShouldGetUserByLogin()
        {
            await AuthenticateAsync();

            await using var scope = _factory.Services.CreateAsyncScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var user = User.Create(
                new Guid("3e32f7c7-e968-4ab2-b73d-360837cd5423"),
                new Login("d5889cfb-28a0-464a-8460-6f0f8ec01556@gmail.com"),
                "3ec10ece-3f77-4070-acba-d0902efda851");
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();

            using var response = await HttpClient.GetAsync($"api/users/{user.Login.Value}");
            response.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            var reponseBody = await response.Content.ReadFromJsonAsync<ResponseBody<GetUserByLoginResponse>>();

            reponseBody.Should().NotBeNull()
                    .And.Subject.As<ResponseBody<GetUserByLoginResponse>>()
                .Data.Should().NotBeNull()
                    .And.Subject.As<GetUserByLoginResponse>();

            reponseBody!.Data!.Id.Should().Be(user.Id);
            reponseBody!.Data!.Login.Should().Be(user.Login.Value);
            reponseBody!.Data!.PhoneNumber.Should().Be(user.PhoneNumber);
        }

        [Fact]
        public async Task ShouldAssignEquipmentToUser()
        {
            await AuthenticateAsync();

            await using var scope = _factory.Services.CreateAsyncScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // create user
            var user = User.Create(
                new Guid("ab11129a-7265-471f-b2c1-8f8c0f3bbd84"),
                new Login("44c22d86-e461-4c0c-a08f-5e5dc493c5bc@gmail.com"),
                "4bcd9ad8-ea56-4575-9804-88c7780e7d03");
            await db.Users.AddAsync(user);

            var date = new DateTimeOffset(
                2024, 1, 1,
                19, 19, 5,
                new TimeSpan());

            // create equipment
            var equipmentType = new EquipmentType(
                new Guid("13547b1b-8cda-49d4-9434-081f22985278"),
                "PC Parts",
                TypeOfEquipment.Equipment,
                date);
            await db.EquipmentTypes.AddAsync(equipmentType);

            var equipment = new Equipment(
                new Guid("1817e99d-3487-481e-994c-5a087066e296"),
                equipmentType.Id,
                "SSD Samsung 500GB",
                date,
                "00023432949",
                1777.21m,
                date);
            await db.Equipments.AddAsync(equipment);

            // save simulated data in db
            await db.SaveChangesAsync();

            // call for assign equipment to user endpoint
            var assignDate = new DateTimeOffset(
                2024, 5, 10,
                20, 33, 5,
                new TimeSpan());

            var request = new AssignEquipmentToUserRequest(
                user.Id,
                equipment.Id,
                assignDate);

            using var response = await HttpClient.PostAsJsonAsync($"api/users/assignEquipment", request);
            response.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            // assert user equipment in db
            var userEquipmentInDb = await db.UserEquipments
                .SingleOrDefaultAsync(x => x.UserId == user.Id);

            userEquipmentInDb.Should().NotBeNull();
            userEquipmentInDb!.EquipmentId.Should().Be(equipment.Id);
            userEquipmentInDb!.AssignedDate.Should().Be(assignDate);

            // assert outbox message in db
            var outboxMessageInDb = await db.OutboxMessages.SingleOrDefaultAsync();
            outboxMessageInDb.Should().NotBeNull();
            outboxMessageInDb!.ProcessedOnUtc.Should().BeNull();
            outboxMessageInDb!.Content.Should().NotBeNullOrWhiteSpace();

            var equipmentAssignedToUserDomainEvent = JsonConvert
                .DeserializeObject<EquipmentAssignedToUserDomainEvent>(outboxMessageInDb!.Content);

            equipmentAssignedToUserDomainEvent.Should().NotBeNull();
            equipmentAssignedToUserDomainEvent!.EquipmentId.Should().Be(equipment.Id);
            equipmentAssignedToUserDomainEvent!.UserId.Should().Be(user.Id);
            equipmentAssignedToUserDomainEvent!.AssignedDate.Should().Be(assignDate);

            // assert outbox message to be processed eventually
            await PollerFacade.AssertEventually(
                new GetProcessedOutboxMessagesProbe(db, outboxMessageInDb),
                10_000);
        }
    }
}
