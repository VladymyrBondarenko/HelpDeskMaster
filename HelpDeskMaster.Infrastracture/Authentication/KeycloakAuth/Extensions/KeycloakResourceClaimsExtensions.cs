using HelpDeskMaster.Infrastracture.Authentication.KeycloakAuth.Common;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Text.Json;

namespace HelpDeskMaster.Infrastracture.Authentication.KeycloakAuth.Extensions
{
    internal static class KeycloakResourceClaimsExtensions
    {
        private const string ClaimValueType = "JSON";

        /// <summary>
        /// Try get Claim from JWT
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="resourcesAccess"></param>
        /// <returns></returns>
        public static bool TryGetRealmResource(
            this IEnumerable<Claim> claims,
            [MaybeNullWhen(false)] out ResourceAccess resourcesAccess)
        {
            var claim = claims.SingleOrDefault(x =>
                x.Type.Equals(KeycloakConstants.RealmAccessClaimType, StringComparison.OrdinalIgnoreCase)
                && x.ValueType.Equals(ClaimValueType, StringComparison.OrdinalIgnoreCase));

            if (claim == null || string.IsNullOrEmpty(claim.Value))
            {
                resourcesAccess = default;
                return false;
            }

            resourcesAccess = JsonSerializer.Deserialize<ResourceAccess>(claim.Value)!;

            return true;
        }
    }
}
