using HelpDeskMaster.Domain.DependencyInjection;
using HelpDeskMaster.Persistence.DependencyInjection;
using HelpDeskMaster.App.DependencyInjection;
using HelpDeskMaster.WebApi.Helpers;
using HelpDeskMaster.WebApi.Middleware;
using HelpDeskMaster.Infrastracture.DependencyInjection;
using HelpDeskMaster.WebApi.Installers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services
    .AddHelpDeskMasterDomain()
    .AddHelpDeskMasterApp()
    .AddHelpDeskMasterInfrastracture(builder.Configuration)
    .AddHelpDeskMasterPersistence(builder.Configuration.GetConnectionString("HdmDbConnection")!);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.PrepareDataPopulation();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();

public partial class Program
{
}