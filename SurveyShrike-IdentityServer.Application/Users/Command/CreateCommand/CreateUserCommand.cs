using MediatR;
using SurveyShrike_IdentityServer.Application.Exceptions.Account;
using SurveyShrike_IdentityServer.Domain.Entities;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// @author Ankit
/// @date - 10/14/2019 11:11:43 AM 
/// </summary>
namespace SurveyShrike_IdentityServer.Application.Users.Command.CreateCommand
{
    public class CreateUserCommand : IRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class Handler : IRequestHandler<CreateUserCommand, Unit>
        {
            private readonly IMediator _mediator;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(UserManager<ApplicationUser> userManager, IMediator mediator)
            {
                _userManager = userManager;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var entity = new ApplicationUser()
                {
                    Email = request.Email,
                    UserName = request.Email,
                    Id = System.Guid.NewGuid().ToString()
                };



                var result = await _userManager.CreateAsync(entity, request.Password);
                if (result.Succeeded)
                {
                    if (request.Email != null)
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(entity);

                        /*var callbackUrl = Url.Action(
                               "ConfirmEmail", "Account",
                               new { userId = user.Id, code = code },
                               protocol: Request.Url.Scheme);

                            await _userManager.SendEmailAsync(entity.Id,
                               "Confirm your account",
                               "Please confirm your account by clicking this link: <a href=\""
                                                               + callbackUrl + "\">link</a>");*/
                    }
                }
                else
                {
                    throw new UserRegistrationFailureException(string.Join("\\n", result.Errors));
                }

                await _mediator.Publish(new UserCreated(), cancellationToken);

                return Unit.Value;
            }
        }
    }
}
