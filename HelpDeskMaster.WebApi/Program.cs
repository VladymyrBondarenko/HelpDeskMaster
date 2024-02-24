using HelpDeskMaster.Domain.DependencyInjection;
using HelpDeskMaster.Persistence.DependencyInjection;
using HelpDeskMaster.App.DependencyInjection;
using HelpDeskMaster.WebApi.Helpers;
using HelpDeskMaster.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddHelpDeskMasterDomain()
    .AddHelpDeskMasterApp()
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

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();

public partial class Program
{
}