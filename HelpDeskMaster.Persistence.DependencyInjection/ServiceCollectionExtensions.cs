using HelpDeskMaster.Domain.Entities.Equipments;
using HelpDeskMaster.Domain.Entities.EquipmentTypes;
using HelpDeskMaster.Domain.Entities.Users;
using HelpDeskMaster.Domain.Entities.WorkCategories;
using HelpDeskMaster.Domain.Entities.WorkDirections;
using HelpDeskMaster.Domain.Entities.WorkRequests;
using HelpDeskMaster.Persistence.Data;
using HelpDeskMaster.Persistence.Data.Repositories;
using HelpDeskMaster.Persistence.Data.Repositories.Equipment;
using HelpDeskMaster.Persistence.Data.Repositories.User;
using HelpDeskMaster.Persistence.Data.Repositories.WorkRequest;
using HelpDeskMaster.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HelpDeskMaster.Persistence.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHelpDeskMasterPersistence(this IServiceCollection services,
            string connectionString)
        {
            services
                .AddScoped<IDbConnectionFactory, DbConnectionFactory>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IWorkCategoryRepository, WorkCategoryRepository>()
                .AddScoped<IWorkDirectionRepository, WorkDirectionRepository>()
                .AddScoped<IEquipmentTypeRepository, EquipmentTypeRepository>()
                .AddScoped<IEquipmentRepository, EquipmentRepository>()
                .AddScoped<IEquipmentComputerInfoRepository, EquipmentComputerInfoRepository>()
                .AddScoped<IComputerEquipmentRepository, ComputerEquipmentRepository>()
                .AddScoped<IUserEquipmentRepository, UserEquipmentRepository>()
                .AddScoped<IWorkRequestRepository, WorkRequestRepository>();

            services.AddSingleton<ConvertDomainEventsToOutboxMessageInterceptor>();

            services.AddDbContextPool<ApplicationDbContext>((sp, opt) =>
            {
                var domainEventsInterceptor = sp.GetService<ConvertDomainEventsToOutboxMessageInterceptor>()!;

                opt.UseNpgsql(connectionString).AddInterceptors(domainEventsInterceptor);
            });

            return services;
        }
    }
}
