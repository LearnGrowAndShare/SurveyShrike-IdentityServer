using System;
using System.Collections.Generic;
using System.Text;


/// <summary>
/// @author Ankit
/// @date - 10/14/2019 10:52:07 AM 
/// </summary>
namespace SurveyShrike_IdentityServer.Application.Exceptions.Account
{
    public class InvalidAccessException : Exception
    {
        public InvalidAccessException()
                : base("You do not have access to the resource.")
        {

        }

    }
}
