using HelpDeskMaster.Domain.DependencyInjection;
using HelpDeskMaster.Persistence.DependencyInjection;
using HelpDeskMaster.WebApi.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(
    typeof(HelpDeskMaster.App.DependencyInjection).Assembly));

builder.Services
    .AddHelpDeskMasterDomain()
    .AddHelpDeskMasterPersistence(builder.Configuration.GetConnectionString("ApplicationDbConnection")!);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.PrepareDataPopulation();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
