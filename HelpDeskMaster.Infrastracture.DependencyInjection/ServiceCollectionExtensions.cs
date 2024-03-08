using HelpDeskMaster.Domain.Authentication;
using HelpDeskMaster.Infrastracture.Authentication;
using HelpDeskMaster.Infrastracture.Authentication.KeycloakAuth.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace HelpDeskMaster.Infrastracture.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHelpDeskMasterInfrastracture(this IServiceCollection services, IConfiguration configuration)
        {
            var authenticationOptions = configuration
                .GetSection(KeycloakAuthenticationOptions.Section)
                .Get<KeycloakAuthenticationOptions>()!;
            services.AddKeycloakAuthentication(authenticationOptions);

            services.AddScoped<IIdentityProvider, IdentityProvider>();

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
                ValidateAudience = keycloakOptions.VerifyTokenAudience ?? true,
                ValidateIssuer = true,
                NameClaimType = "preferred_username",
                RoleClaimType = roleClaimType,
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
    }
}
