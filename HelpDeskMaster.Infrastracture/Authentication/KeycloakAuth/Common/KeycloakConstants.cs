﻿namespace HelpDeskMaster.Infrastracture.Authentication.KeycloakAuth.Common
{
    /// <summary>
    /// Well-known constants
    /// </summary>
    internal static class KeycloakConstants
    {
        /// <summary>
        /// Token endpoint
        /// </summary>
        public const string TokenEndpointPath = "protocol/openid-connect/token";

        /// <summary>
        /// JWT Token Claim - Resource Access
        /// </summary>
        public const string ResourceAccessClaimType = "resource_access";

        /// <summary>
        /// JWT Token Claim - Realm Access
        /// </summary>
        public const string RealmAccessClaimType = "realm_access";
    }
}
