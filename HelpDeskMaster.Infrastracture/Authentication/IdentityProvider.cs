using HelpDeskMaster.Domain.Authentication;
using HelpDeskMaster.Infrastracture.Authentication.KeycloakAuth.Extensions;
using HelpDeskMaster.Infrastracture.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace HelpDeskMaster.Infrastracture.Authentication
{
    internal class IdentityProvider : IIdentityProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IIdentity Current => GetCurrentIdentity();

        private IIdentity GetCurrentIdentity()
        {
            var claims = _httpContextAccessor?.HttpContext?.User.Claims.ToList();

            if(claims == null)
            {
                throw new AuthenticationException();
            }

            var userIdAsStr = claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userIdAsStr))
            {
                throw new AuthenticationException();
            }

            if (!claims.TryGetRealmResource(out var resourceAccess))
            {
                throw new AuthenticationException();
            }

            var roles = resourceAccess.Roles.ToHashSet();

            return new Identity(new Guid(userIdAsStr), roles);
        }
    }
}
