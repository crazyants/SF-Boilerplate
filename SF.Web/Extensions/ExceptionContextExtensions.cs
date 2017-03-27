using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using SF.Core.Errors.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SF.Web.Extensions
{
    public static class ExceptionContextExtensions
    {
        public static int GetStatusCode(this Exception ex, HttpContext context)
        {
            if (ex is UnauthorizedAccessException)
            {
                return context.User.Identity.IsAuthenticated
                    ? (int)HttpStatusCode.Forbidden
                    : (int)HttpStatusCode.Unauthorized;
            }

            if (ex is ValidationException)
            {
                return (int)HttpStatusCode.BadRequest;
            }

            if (ex is EntityNotFoundException)
            {
                return (int)HttpStatusCode.NotFound;
            }

            return (int)HttpStatusCode.InternalServerError;
        }
    }

}
