using Microsoft.Extensions.Configuration;

namespace HelpDeskMaster.Infrastracture.Authentication.KeycloakAuth.Configuration
{
    /// <summary>
    /// Installation options provided by Keycloak
    /// </summary>
    /// <remarks>
    /// See "/.well-known/openid-configuration"
    /// </remarks>
    public class KeycloakInstallationOptions
    {
        private string? authServerUrl;
        private bool? verifyTokenAudience;
        private bool? validateIssuer;
        private TimeSpan? tokenClockSkew;
        private string? sslRequired;

        /// <summary>
        /// Authorization server URL
        /// </summary>
        /// <example>
        /// "auth-server-url": "http://localhost:8088/auth/"
        /// </example>
        [ConfigurationKeyName("auth-server-url")]
        public string AuthServerUrl
        {
            get => authServerUrl ?? string.Empty;
            set => authServerUrl = value;
        }

        /// <summary>
        /// Keycloak Realm
        /// </summary>
        public string Realm { get; set; } = string.Empty;

        /// <summary>
        /// Resource as client id
        /// </summary>
        /// <example>
        /// "resource": "client-id"
        /// </example>
        public string Resource { get; set; } = string.Empty;

        /// <summary>
        /// Audience verification
        /// </summary>
        [ConfigurationKeyName("verify-token-audience")]
        public bool VerifyTokenAudience
        {
            get => verifyTokenAudience ?? true;
            set => verifyTokenAudience = value;
        }

        /// <summary>
        /// Validate issuer
        /// </summary>
        [ConfigurationKeyName("verify-token-issuer")]
        public bool ValidateIssuer 
        { 
            get => validateIssuer ?? true; 
            set => validateIssuer = value; 
        }

        /// <summary>
        /// Credentials, defined for private client
        /// </summary>
        public KeycloakClientInstallationCredentials Credentials { get; set; } = new();

        /// <summary>
        ///     Optional
        /// </summary>
        /// <remarks>
        ///     - Default: 0 seconds
        /// </remarks>
        [ConfigurationKeyName("token-clock-skew")]
        public TimeSpan TokenClockSkew
        {
            get => tokenClockSkew ?? TimeSpan.Zero;
            set => tokenClockSkew = value;
        }

        /// <summary>
        /// Require HTTPS
        /// </summary>
        [ConfigurationKeyName("ssl-required")]
        public string SslRequired
        {
            get => sslRequired ?? "external";
            set => sslRequired = value;
        }

        /// <summary>
        /// Realm URL
        /// </summary>
        public string KeycloakUrlRealm => $"{NormalizeUrl(AuthServerUrl)}/realms/{Realm}";

        private static string NormalizeUrl(string url)
        {
            var urlNormalized = !url.EndsWith('/') ? url : url.TrimEnd('/');

            return urlNormalized;
        }
    }
}
