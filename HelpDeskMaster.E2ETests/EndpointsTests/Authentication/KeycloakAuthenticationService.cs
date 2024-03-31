using Newtonsoft.Json;

namespace HelpDeskMaster.E2ETests.EndpointsTests.Authentication
{
    public class KeycloakAuthenticationService
    {
        private readonly static string _keycloakDirectAuthRoute = "{0}realms/hdm-realm/protocol/openid-connect/token";

        private readonly string _authServerUrl;

        public KeycloakAuthenticationService(string authServerUrl)
        {
            _authServerUrl = authServerUrl;
        }

        public async Task<KeycloakAuthenticationResponse> AuthenticateToKeycloak()
        {
            using var httpClient = new HttpClient();

            var values = new Dictionary<string, string>
            {
                { "client_id", "hdm-client" },
                { "username", "hdm-client-user" },
                { "password", "password" },
                { "grant_type", "password" },
                { "client_secret", "WNMzQVpMkjskGVTZCJB4T5SQ6xPQjJzg" }
            };

            var response = await httpClient.PostAsync(
                string.Format(_keycloakDirectAuthRoute, _authServerUrl),
                new FormUrlEncodedContent(values));
            response.EnsureSuccessStatusCode();

            var authResponse = JsonConvert.DeserializeObject<KeycloakAuthenticationResponse>(
                await response.Content.ReadAsStringAsync());

            if (authResponse == null) throw new Exception("Authentication response body was empty");

            return authResponse;
        }
    }
}