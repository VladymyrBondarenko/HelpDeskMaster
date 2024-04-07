using HelpDeskMaster.Domain.Authentication;
using HelpDeskMaster.Domain.Entities.Users;
using HelpDeskMaster.Infrastracture.Authentication.KeycloakAuth.Extensions;
using HelpDeskMaster.Infrastracture.Exceptions;
using HelpDeskMaster.Persistence.Data;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace HelpDeskMaster.Infrastracture.Authentication
{
    internal class IdentityProvider : IIdentityProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _dbContext;

        public IdentityProvider(IHttpContextAccessor httpContextAccessor,
            IUserService userService, ApplicationDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _dbContext = dbContext;
        }

        public async Task<IIdentity> GetIdentityAsync(CancellationToken cancellationToken)
        {
            var claims = _httpContextAccessor?.HttpContext?.User.Claims.ToList();

            if (claims == null)
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

            var email = claims.SingleOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new AuthenticationException();
            }

            var phoneNumber = claims.SingleOrDefault(x => x.Type == ClaimTypes.MobilePhone)?.Value;

            await syncUserInDb(email, phoneNumber, cancellationToken);

            var roles = resourceAccess.Roles.ToHashSet();

            return new Identity(new Guid(userIdAsStr), roles);
        }

        private async Task syncUserInDb(string email, string? phoneNumber, 
            CancellationToken cancellationToken)
        {
            var user = User.Create(new Login(email), phoneNumber);

            await _userService.CreateOrUpdateUserAsync(user, cancellationToken);

            await _dbContext.SaveChangesAsync();
        }
    }
}
