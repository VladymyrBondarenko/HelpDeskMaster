using FluentValidation;
using HelpDeskMaster.App.Behaviours;
using Microsoft.Extensions.DependencyInjection;

namespace HelpDeskMaster.App.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHelpDeskMasterApp(this IServiceCollection services)
        {
            var targetAssembly = typeof(HelpDeskMasterAppRoot).Assembly;

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(targetAssembly);
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            services.AddValidatorsFromAssembly(targetAssembly, includeInternalTypes: true);

            return services;
        }
    }
}
