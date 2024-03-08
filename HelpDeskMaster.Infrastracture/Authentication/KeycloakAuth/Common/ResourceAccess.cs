using System.Text.Json.Serialization;

namespace HelpDeskMaster.Infrastracture.Authentication.KeycloakAuth.Common
{
    internal class ResourceAccess
    {
        /// <summary>
        /// </summary>
        [JsonPropertyName("roles")]
        public List<string> Roles { get; init; } = new();
    }
}
