using System;

namespace SF.Core.Errors.Internal
{
    internal class Defaults
    {
        internal class ErrorMessage
        {
            internal const string Key = "";
            internal const string NullString = "null";
        }

        internal class BaseException
        {
        }

        internal class NotFoundException
        {
            internal const string Title = "Not found.";
            internal const string Code = "NFOUND001";
        }

        internal class UnauthorizedException
        {
            internal const string Title = "Access denied.";
            internal const string Code = "UNAUTH001";
        }

        internal class ValidationException
        {
            internal const string Title = "Bad request.";
            internal const string Code = "UNVALI001";
        }

        internal class EntityNotFoundException
        {
            internal const string Title = "Entity Not found.";
            internal const string Code = "UNVALI001";
        }
        internal class ServerErrorException
        {
            internal const string Title = "Server Error.";
            internal const string Code = "500";
        }
    }
}
