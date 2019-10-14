using MediatR;

/// <summary>
/// @author Ankit
/// @date - 10/14/2019 11:35:07 AM 
/// </summary>
namespace SurveyShrike_IdentityServer.Application.Users.Command.Queries.Authentication
{
    public class AuthenticationModel : IRequest<AuthorizedUserDTO>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberLogin { get; set; }
    }
}
