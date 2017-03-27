using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Web.Base.Mappers.ModelBinders
{
    /// <summary>
    /// An <see cref="IModelBinderProvider"/> for deserializing the request body using a formatter, using the provided destination type in the MapFromBody attribute.
    /// </summary>
    public class MappedBodyModelBinderProvider : IModelBinderProvider
    {
        private readonly IMapper _mapper;
        private readonly IList<IInputFormatter> _formatters;
        private readonly IHttpRequestStreamReaderFactory _readerFactory;

        /// <summary>
        /// Creates a new <see cref="BodyModelBinderProvider"/>.
        /// </summary>
        /// <param name="formatters">The list of <see cref="IInputFormatter"/>.</param>
        /// <param name="readerFactory">The <see cref="IHttpRequestStreamReaderFactory"/>.</param>
        public MappedBodyModelBinderProvider(IMapper mapper, IList<IInputFormatter> formatters, IHttpRequestStreamReaderFactory readerFactory)
        {
            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            if (formatters == null)
            {
                throw new ArgumentNullException(nameof(formatters));
            }

            if (readerFactory == null)
            {
                throw new ArgumentNullException(nameof(readerFactory));
            }

            _mapper = mapper;
            _formatters = formatters;
            _readerFactory = readerFactory;
        }

        /// <inheritdoc />
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.BindingInfo.BindingSource != null &&
                context.BindingInfo.BindingSource.CanAcceptDataFrom(BindingSource.Body))
            {
                if (_formatters.Count == 0)
                {
                    throw new InvalidOperationException(string.Format("{0}.{1}' must not be empty. At least one '{2}' is required to bind from the body.",
                        typeof(MvcOptions).FullName,
                        nameof(MvcOptions.InputFormatters),
                        typeof(IInputFormatter).FullName));
                }

                return new MappedBodyModelBinder(context.MetadataProvider, _mapper, _formatters, _readerFactory);
            }

            return null;
        }
    }
}
