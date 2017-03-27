using System.Linq;
using FluentValidation;
using MediatR;

namespace SF.Web.Validation
{
    //public class ValidationRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    //{
    //    private readonly IRequestHandler<TRequest, TResponse> _innerHander;
    //    private readonly IValidator<TRequest>[] _validators;

    //    public ValidationRequestHandler(IRequestHandler<TRequest, TResponse> innerHandler, IValidator<TRequest>[] validators)
    //    {
    //        _validators = validators;
    //        _innerHander = innerHandler;
    //    }

    //    public TResponse Handle(TRequest message)
    //    {
    //        var context = new ValidationContext(message);

    //        var failures =
    //            _validators.Select(v => v.Validate(context)).SelectMany(r => r.Errors).Where(f => f != null).ToList();

    //        if (failures.Any())
    //        {
    //            throw new ValidationException(failures);
    //        }

    //        return _innerHander.Handle(message);
    //    }
    //}
}