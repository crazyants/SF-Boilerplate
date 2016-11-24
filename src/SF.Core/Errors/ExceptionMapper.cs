using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SF.Core.Errors.Exceptions;
using SF.Core.Errors.Internal;

namespace SF.Core.Errors
{
    public abstract class ExceptionMapper : IExceptionMapper
    {
        private static ConcurrentDictionary<Type, Action<Error, Exception>> _errorMappings;

        protected ExceptionMapper()
        {
            _errorMappings = new ConcurrentDictionary<Type, Action<Error, Exception>>();
            CreateMap<Exception>(CreateDefaultMap);
            CreateMap<NotFoundException>(CreateNotFoundMap);
            CreateMap<UnauthorizedException>(CreateUnauthorizedMap);
            CreateMap<ValidationException>(CreateValidationMap);
            Configure();
        }

        protected abstract void Configure();

        protected abstract void CreateDefaultMap(Error error, Exception exception);

        protected virtual void CreateNotFoundMap(Error error, NotFoundException exception)
        {
            error.Title = Defaults.NotFoundException.Title;
            error.Code = Defaults.NotFoundException.Code;
            error.Status = 404;
            error.ExtraParameters = exception.Messages.ToDictionary(ms => ms.Key, ms => (object)ms.Value);
        }

        protected virtual void CreateUnauthorizedMap(Error error, UnauthorizedException exception)
        {
            error.Title = Defaults.UnauthorizedException.Title;
            error.Code = Defaults.UnauthorizedException.Code;
            error.Status = 403;
            error.ExtraParameters = exception.Messages.ToDictionary(ms => ms.Key, ms => (object)ms.Value);
        }

        protected virtual void CreateValidationMap(Error error, ValidationException exception)
        {
            error.Title = Defaults.ValidationException.Title;
            error.Code = Defaults.ValidationException.Code;
            error.Status = 400;
            error.ExtraParameters = exception.Messages.ToDictionary(ms => ms.Key, ms => (object)ms.Value);
        }

        /// <summary>
        /// Used to map an exception to a specific status code
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="statusCode"></param>
        protected void CreateMap<TException>(int statusCode) where TException : Exception
        {
            Action<Error, Exception> action = (x, y) => x.Status = statusCode;
            _errorMappings.TryAdd(typeof(TException), action);
        }

        protected void CreateMap<TException>(Action<Error, TException> configError) where TException : Exception
        {
            Action<Error, Exception> action = (x, y) => configError(x, (TException)y);
            _errorMappings.TryAdd(typeof(TException), action);
        }

        protected Error Map(Exception exception)
        {
            var type = exception?.GetType() ?? typeof(Exception);
            var error = new Error();

            if (_errorMappings.ContainsKey(type))
                _errorMappings[type](error, exception);
            else if (_errorMappings.ContainsKey(typeof(Exception)))
                _errorMappings[typeof(Exception)](error, exception);
            else
                error = null;

            return error;
        }

        public virtual Error Resolve(Exception exception)
        {
            var error = Map(exception);

            var baseException = exception as BaseException;
            if (baseException != null)
                error.ExtraParameters = baseException.Messages.ToDictionary(x => x.Key, x => (object)x.Value);

            return error;
        }
    }
}
