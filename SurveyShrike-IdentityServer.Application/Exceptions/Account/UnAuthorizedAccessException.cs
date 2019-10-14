using System;
using System.Collections.Generic;
using System.Text;


/// <summary>
/// @author Ankit
/// @date - 10/14/2019 10:52:21 AM 
/// </summary>
namespace SurveyShrike_IdentityServer.Application.Exceptions.Account
{
    public class UnAuthorizedAccessException : Exception
    {
        public UnAuthorizedAccessException()
                : base("Unauthorized access.")
        {

        }

    }
}
