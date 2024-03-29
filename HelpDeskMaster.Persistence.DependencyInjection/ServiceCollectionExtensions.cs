﻿using HelpDeskMaster.Domain.Entities.WorkCategories;
using HelpDeskMaster.Domain.Entities.WorkDirections;
using HelpDeskMaster.Persistence.Data;
using HelpDeskMaster.Persistence.Data.Repositories;
using HelpDeskMaster.Persistence.Data.Repositories.WorkRequest;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HelpDeskMaster.Persistence.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHelpDeskMasterPersistence(this IServiceCollection services,
            string connectionString)
        {
           services.AddDbContextPool<ApplicationDbContext>(opt =>
            {
                opt.UseNpgsql(connectionString);
            });

            services
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IWorkCategoryRepository, WorkCategoryRepository>()
                .AddScoped<IWorkDirectionRepository, WorkDirectionRepository>();

            return services;
        }
    }
}
