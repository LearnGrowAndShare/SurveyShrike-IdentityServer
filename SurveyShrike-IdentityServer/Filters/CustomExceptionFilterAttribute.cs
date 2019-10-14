using System;
using System.Net;
using SurveyShrike_IdentityServer.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SurveyShrike_IdentityServer.Application.Exceptions.Account;

namespace SurveyShrike_IdentityServer.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationException)
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new JsonResult(
                    ((ValidationException)context.Exception).Failures);

                return;
            }

            var code = HttpStatusCode.InternalServerError;

          
            if (context.Exception is InvalidCredentialsException)
            {
                code = HttpStatusCode.BadRequest;
            }
            else if (context.Exception is InvalidAccessException)
            {
                code = HttpStatusCode.Forbidden;
            }
            else if (context.Exception is AccountLockedoutException)
            {
                code = HttpStatusCode.Locked;
            }
            else if (context.Exception is UnAuthorizedAccessException)
            {
                code = HttpStatusCode.Unauthorized;
            }

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)code;
            context.Result = new JsonResult(new
            {
                error = new[] { context.Exception.Message }
                //stackTrace = context.Exception.StackTrace
            });
        }
    }
}
