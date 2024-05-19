using Hangfire;
using Hangfire.PostgreSql;
using HelpDeskMaster.Domain.Authentication;
using HelpDeskMaster.Infrastracture.Authentication;
using HelpDeskMaster.Infrastracture.Authentication.KeycloakAuth.Configuration;
using HelpDeskMaster.Infrastracture.BackgroundJobs;
using HelpDeskMaster.Infrastracture.Mailing;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace HelpDeskMaster.Infrastracture.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHelpDeskMasterInfrastracture(this IServiceCollection services,
            IConfiguration configuration)
        {
            // Keycloak authentication configuring
            var authenticationOptions = configuration
                .GetSection(KeycloakAuthenticationOptions.Section)
                .Get<KeycloakAuthenticationOptions>()!;
            services.AddKeycloakAuthentication(authenticationOptions);
            services.AddSingleton(authenticationOptions);
            services.AddScoped<IIdentityProvider, IdentityProvider>();

            // Email sending configuring
            var emailSenderOptions = configuration
                .GetSection(EmailSenderOptions.Section)
                .Get<EmailSenderOptions>()!;
            services.AddSingleton(emailSenderOptions);
            services.AddScoped<IEmailService, EmailService>();

            // Background jobs configuring
            services.AddBackgroundJobs(configuration);

            return services;
        }

        /// <summary>
        /// Adds keycloak authentication services.
        /// </summary>
        private static AuthenticationBuilder AddKeycloakAuthentication(
            this IServiceCollection services,
            KeycloakAuthenticationOptions keycloakOptions)
        {
            const string roleClaimType = "role";
            var validationParameters = new TokenValidationParameters
            {
                ClockSkew = keycloakOptions.TokenClockSkew,
                ValidateAudience = keycloakOptions.VerifyTokenAudience,
                ValidateIssuer = keycloakOptions.ValidateIssuer,
                NameClaimType = "preferred_username",
                RoleClaimType = roleClaimType
            };

            return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opts =>
                {
                    var sslRequired = string.IsNullOrWhiteSpace(keycloakOptions.SslRequired)
                        || keycloakOptions.SslRequired
                            .Equals("external", StringComparison.OrdinalIgnoreCase);

                    opts.Authority = keycloakOptions.KeycloakUrlRealm;
                    opts.Audience = keycloakOptions.Resource;
                    opts.TokenValidationParameters = validationParameters;
                    opts.RequireHttpsMetadata = sslRequired;
                    opts.SaveToken = true;
                });
        }

        private static void AddBackgroundJobs(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddHangfire(config => 
                config.UsePostgreSqlStorage(options =>
                    options.UseNpgsqlConnection(configuration.GetConnectionString("HdmDbConnection"))));

            services.AddHangfireServer(options => options.SchedulePollingInterval = TimeSpan.FromSeconds(1));

            services.AddScoped<IProcessOutboxMessagesJob, ProcessOutboxMessagesJob>();
        }
    }
}
