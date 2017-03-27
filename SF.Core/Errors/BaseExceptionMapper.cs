using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace SF.Core.Errors
{
    public class BaseExceptionMapper : ExceptionMapper
    {
        protected override void Configure()
        {
            CreateMap<NotImplementedException>((error, ex) =>
            {
                error.Status = 404;
                error.Title = "Methode call not allowed";
                error.Code = "NOTF001";
            });

            CreateMap<UnauthorizedAccessException>(403);
        }

        protected override void CreateDefaultMap(Error error, Exception exception)
        {
            error.Status = 500;
            error.Title = "We are currently experiencing a technical error";
            error.Code = "TECHE001";
        }


    }
}
