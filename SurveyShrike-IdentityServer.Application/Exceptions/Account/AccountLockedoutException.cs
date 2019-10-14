using System;
using System.Collections.Generic;
using System.Text;


/// <summary>
/// @author Ankit
/// @date - 10/14/2019 10:51:59 AM 
/// </summary>
namespace SurveyShrike_IdentityServer.Application.Exceptions.Account
{
    public class AccountLockedoutException : Exception
    {
        public AccountLockedoutException()
                : base("Account has been locked. Please reset the password.")
        {

        }

    }
}
