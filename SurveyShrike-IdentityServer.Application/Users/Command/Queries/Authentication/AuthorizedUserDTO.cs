using System;
using System.Collections.Generic;
using System.Text;


/// <summary>
/// @author Ankit
/// @date - 10/14/2019 11:34:45 AM 
/// </summary>
namespace SurveyShrike_IdentityServer.Application.Users.Command.Queries.Authentication
{
    public class AuthorizedUserDTO
    {
        public AuthorizedUserDTO(string redirectUrl)
        {
            this.RedirectUrl = redirectUrl;
        }

        public string RedirectUrl { get; private set; }
    }
}
