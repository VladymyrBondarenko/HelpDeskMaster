using FluentAssertions;
using HelpDeskMaster.WebApi.Contracts;
using System.Net.Http.Json;
using HelpDeskMaster.WebApi.Contracts.Equipment.Responses;
using Xunit;
using HelpDeskMaster.WebApi.Contracts.Equipment.Requests;
using HelpDeskMaster.Domain.Entities.EquipmentTypes;
using HelpDeskMaster.WebApi.Contracts.WorkRequest.Responses;
using Microsoft.Extensions.DependencyInjection;
using HelpDeskMaster.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskMaster.E2ETests.EndpointsTests
{
    public class EquipmentTypeEndpointTests : HdmEndpointTestBase
    {
        private readonly HdmServerApplicationFactory _factory;

        public EquipmentTypeEndpointTests(HdmServerApplicationFactory factory) : base(factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ShouldReturnListOfEquipmentTypes()
        {
            await AuthenticateAsync();

            using var response = await HttpClient.GetAsync("api/equipmentTypes");
            response.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            var reponseBody = await response.Content.ReadFromJsonAsync<ResponseBody<GetAllEquipmentTypesResponse>>();

            reponseBody.Should().NotBeNull()
                    .And.Subject.As<ResponseBody<GetAllEquipmentTypesResponse>>()
                .Data.Should().NotBeNull();
        }

        [Fact]
        public async Task ShouldCreateNewEquipmentType()
        {
            await AuthenticateAsync();

            var request = new CreateEquipmentTypeRequest("PC Parts", TypeOfEquipment.Equipment);

            using var response = await HttpClient.PostAsJsonAsync("api/equipmentTypes",
                request, CancellationToken.None);
            response.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            var reponseBody = await response.Content.ReadFromJsonAsync<ResponseBody<CreateEquipmentTypeResponse>>();

            reponseBody.Should().NotBeNull()
                    .And.Subject.As<ResponseBody<CreateEquipmentTypeResponse>>()
                .Data.Should().NotBeNull()
                    .And.Subject.As<CreateEquipmentTypeResponse>()
                .Title.Should().Be(request.Title);
            reponseBody!.Data.Id.Should().NotBeEmpty();

            await using var scope = _factory.Services.CreateAsyncScope();
            var equipmentTypeInDb = await scope.ServiceProvider.GetRequiredService<ApplicationDbContext>()
                .EquipmentTypes
                .SingleOrDefaultAsync(x => x.Id == reponseBody.Data.Id);
            equipmentTypeInDb.Should().NotBeNull();
        }
    }
}
