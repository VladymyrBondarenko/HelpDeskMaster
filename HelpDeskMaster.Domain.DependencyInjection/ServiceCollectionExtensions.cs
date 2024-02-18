using HelpDeskMaster.Domain.Authentication;
using HelpDeskMaster.Domain.Authorization;
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
                .AddScoped<IWorkCategoryService, WorkCategoryService>()
                .AddScoped<IWorkDirectionService, WorkDirectionService>();

            services.AddScoped<IIntentionResolver, ManageWorkCategoryIntentionResolver>()
                .AddScoped<IIntentionResolver, ManageWorkDirectionIntentionResolver>()
                .AddScoped<IIntentionManager, IntentionManager>()
                .AddScoped<IIdentityProvider, IdentityProvider>();

            return services;
        }
    }
}
