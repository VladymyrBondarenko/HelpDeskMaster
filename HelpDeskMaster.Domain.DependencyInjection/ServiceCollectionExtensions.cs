using HelpDeskMaster.Domain.Authorization;
using HelpDeskMaster.Domain.Entities.Equipments;
using HelpDeskMaster.Domain.Entities.Equipments.Intentions;
using HelpDeskMaster.Domain.Entities.EquipmentTypes;
using HelpDeskMaster.Domain.Entities.EquipmentTypes.Intentions;
using HelpDeskMaster.Domain.Entities.Users;
using HelpDeskMaster.Domain.Entities.Users.Intentions;
using HelpDeskMaster.Domain.Entities.WorkCategories;
using HelpDeskMaster.Domain.Entities.WorkCategories.Intentions;
using HelpDeskMaster.Domain.Entities.WorkDirections;
using HelpDeskMaster.Domain.Entities.WorkDirections.Intentions;
using Microsoft.Extensions.DependencyInjection;

namespace HelpDeskMaster.Domain.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHelpDeskMasterDomain(this IServiceCollection services)
        {
            services
                .AddScoped<IUserService, UserService>()
                .AddScoped<IWorkCategoryService, WorkCategoryService>()
                .AddScoped<IWorkDirectionService, WorkDirectionService>()
                .AddScoped<IEquipmentTypeService, EquipmentTypeService>()
                .AddScoped<IEquipmentService, EquipmentService>()
                .AddScoped<IComputerEquipmentService, ComputerEquipmentService>()
                .AddScoped<IUserEquipmentService, UserEquipmentService>();

            services.AddScoped<IIntentionResolver, ManageWorkCategoryIntentionResolver>()
                .AddScoped<IIntentionResolver, ManageWorkDirectionIntentionResolver>()
                .AddScoped<IIntentionResolver, ManageUserIntentionResolver>()
                .AddScoped<IIntentionResolver, ManageEquipmentTypeIntentionResolver>()
                .AddScoped<IIntentionResolver, ManageEquipmentIntentionResolver>()
                .AddScoped<IIntentionResolver, ManageComputerEquipmentIntentionResolver>()
                .AddScoped<IIntentionResolver, ManageEquipmentOwnerIntentionResolver>()
                .AddScoped<IIntentionManager, IntentionManager>();

            return services;
        }
    }
}
