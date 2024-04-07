using HelpDeskMaster.Domain.Authentication;
using HelpDeskMaster.Domain.Entities.Users;
using HelpDeskMaster.Infrastracture.Authentication.KeycloakAuth.Extensions;
using HelpDeskMaster.Infrastracture.Exceptions;
using HelpDeskMaster.Persistence.Data.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace HelpDeskMaster.Infrastracture.Authentication
{
    internal class IdentityProvider : IIdentityProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public IdentityProvider(IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
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
            var existingUser = await _userRepository.GetAsync(email, cancellationToken);

            if (existingUser != null)
            {
                if (phoneNumber != existingUser.PhoneNumber)
                {
                    existingUser.UpdatePhoneNumber(phoneNumber);
                    _userRepository.Update(existingUser);

                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }
            }
            else
            {
                var newUser = User.Create(new Login(email), phoneNumber);
                await _userRepository.InsertAsync(newUser, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
