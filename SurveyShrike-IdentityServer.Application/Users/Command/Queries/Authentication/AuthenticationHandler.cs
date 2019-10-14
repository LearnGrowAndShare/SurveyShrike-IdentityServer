
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using SurveyShrike_IdentityServer.Application.Interfaces;
using SurveyShrike_IdentityServer.Application.Exceptions.Account;
using SurveyShrike_IdentityServer.Domain.Entities;

/// <summary>
/// @author Ankit
/// @date - 10/14/2019 11:32:49 AM 
/// </summary>
namespace SurveyShrike_IdentityServer.Application.Users.Command.Queries.Authentication
{
    public class AuthenticationHandler : IRequestHandler<AuthenticationModel, AuthorizedUserDTO>
    {
        private readonly IClientStore _clientStore;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
 

        public AuthenticationHandler(IClientStore clientStore,
                                     UserManager<ApplicationUser> userManager,
                                     SignInManager<ApplicationUser> signInManager)
        {
          
            _clientStore = clientStore;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<AuthorizedUserDTO> Handle(AuthenticationModel request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Username);

            if (user == null)
            {
                throw new InvalidCredentialsException();
            }
            else
            {
                var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberLogin, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    return new AuthorizedUserDTO(request.ReturnUrl);
                }
                else if (result.IsLockedOut)
                {
                    throw new AccountLockedoutException();
                }
                else if (result.IsNotAllowed)
                {
                    throw new InvalidAccessException();
                }
                else
                {
                    throw new InvalidCredentialsException();
                }

            }

        }


    }
}
