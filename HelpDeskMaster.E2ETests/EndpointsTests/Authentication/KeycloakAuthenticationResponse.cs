using Newtonsoft.Json;

namespace HelpDeskMaster.E2ETests.EndpointsTests.Authentication
{
    [JsonObject]
    public class KeycloakAuthenticationResponse
    {
        [JsonProperty("access_token")]
        public string? AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string? RefreshToken { get; set; }
    }
}