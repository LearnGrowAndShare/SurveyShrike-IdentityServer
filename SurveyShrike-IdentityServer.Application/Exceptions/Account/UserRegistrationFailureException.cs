using System;

/// <summary>
/// @author Ankit
/// @date - 10/14/2019 10:52:29 AM 
/// </summary>
namespace SurveyShrike_IdentityServer.Application.Exceptions.Account
{
    public class UserRegistrationFailureException : Exception
    {
        public UserRegistrationFailureException(string error)
                : base(error)
        {

        }


    }
}
