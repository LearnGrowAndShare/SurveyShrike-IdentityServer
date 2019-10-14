using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SurveyShrike_IdentityServer.Application.Users.Command.CreateCommand;
using SurveyShrike_IdentityServer.Application.Users.Command.Queries.Authentication;
using System.Net;
using System.Threading.Tasks;

namespace SurveyShrike_IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticateController : BaseController
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IHostingEnvironment _environment;
        public AuthenticateController(
            IIdentityServerInteractionService interaction,
            IHostingEnvironment environment)
        {
            _interaction = interaction;
            _environment = environment;
        }


        [HttpPost]
        [ProducesResponseType(typeof(AuthorizedUserDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Login([FromBody]AuthenticationModel request)
        {
            var context = await _interaction.GetAuthorizationContextAsync(request.ReturnUrl);
            if (context != null)
            {
                await _interaction.GrantConsentAsync(context, ConsentResponse.Denied);
                return Ok(await Mediator.Send(request));
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Register([FromBody]CreateUserCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout(string logoutId)
        {
            var context = await _interaction.GetLogoutContextAsync(logoutId);
            bool showSignoutPrompt = true;

            if (context?.ShowSignoutPrompt == false)
            {
                // it's safe to automatically sign-out
                showSignoutPrompt = false;
            }

            if (User?.Identity.IsAuthenticated == true)
            {
                // delete local authentication cookie
                await HttpContext.SignOutAsync();
                await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            }

            // no external signout supported for now (see \Quickstart\Account\AccountController.cs TriggerExternalSignout)
            return Ok(new
            {
                showSignoutPrompt,
                ClientName = string.IsNullOrEmpty(context?.ClientName) ? context?.ClientId : context?.ClientName,
                context?.PostLogoutRedirectUri,
                context?.SignOutIFrameUrl,
                logoutId
            });
        }

        [HttpGet]
        [Route("Error")]
        public async Task<IActionResult> Error(string errorId)
        {
            // retrieve error details from identityserver
            var message = await _interaction.GetErrorContextAsync(errorId);

            if (message != null)
            {
                if (!_environment.IsDevelopment())
                {
                    // only show in development
                    message.ErrorDescription = null;
                }
            }

            return Ok(message);
        }
    }
}
