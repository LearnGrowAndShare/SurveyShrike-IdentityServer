using FluentValidation;
using SurveyShrike_IdentityServer.Application.Infrastructure.Validation;
using SurveyShrike_IdentityServer.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


/// <summary>
/// @author Ankit
/// @date - 10/14/2019 11:24:21 AM 
/// </summary>
namespace SurveyShrike_IdentityServer.Application.Users.Command.CreateCommand
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {

        private readonly IIdentityServerDbContext _context;
        public CreateUserCommandValidator(IIdentityServerDbContext context)
        {
            _context = context;

            RuleFor(x => x.Email).NotEmpty().EmailAddress()
                .SetValidator(new UniqueIdentityDBValidator<CreateUserCommand>(context, true)); ;
            RuleFor(x => x.Password).NotEmpty()
                    .Matches("[A-Z]").WithMessage("Password should have atleast one 1 upper case.")
                    .Matches("[0-9]").WithMessage("Password should have atleast one 1 number.");
        }
    }
}
