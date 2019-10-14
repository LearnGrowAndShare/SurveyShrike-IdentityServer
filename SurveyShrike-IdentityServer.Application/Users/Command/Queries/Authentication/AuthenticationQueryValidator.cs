using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;


/// <summary>
/// @author Ankit
/// @date - 10/14/2019 11:34:15 AM 
/// </summary>
namespace SurveyShrike_IdentityServer.Application.Users.Command.Queries.Authentication
{
    public class AuthenticationQueryValidator : AbstractValidator<AuthenticationModel>
    {
        public AuthenticationQueryValidator()
        {
            RuleFor(v => v.Username).NotEmpty().MinimumLength(5);
            RuleFor(v => v.Password).NotEmpty();
            RuleFor(v => v.ReturnUrl).NotEmpty();
        }
    }
}
