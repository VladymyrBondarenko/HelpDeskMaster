using FluentAssertions;
using HelpDeskMaster.Domain.Entities.Equipments;
using HelpDeskMaster.Domain.Entities.EquipmentTypes;
using HelpDeskMaster.Domain.Entities.Users;
using HelpDeskMaster.Persistence.Data;
using HelpDeskMaster.WebApi.Contracts.User.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Xunit;

namespace HelpDeskMaster.E2ETests.EndpointsTests
{
    public class UserEndpointTests : HdmEndpointTestBase
    {
        private readonly HdmServerApplicationFactory _factory;

        public UserEndpointTests(HdmServerApplicationFactory factory) : base(factory)
        {
            _factory = factory;
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
                "+380919232134");
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

            // check for user equipment in db
            var userEquipmentInDb = await db.UserEquipments
                .SingleOrDefaultAsync(x => x.UserId == user.Id);

            userEquipmentInDb.Should().NotBeNull();
            userEquipmentInDb!.EquipmentId.Should().Be(equipment.Id);
            userEquipmentInDb!.AssignedDate.Should().Be(assignDate);
        }
    }
}
