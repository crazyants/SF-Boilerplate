using AutoMapper;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SF.Web.Base.Mappers.ModelBinders
{
    public class MappedBodyModelBinder : IModelBinder
    {
        private readonly IModelMetadataProvider _modelMetadataProvider;
        private readonly IMapper _mapper;
        private readonly IList<IInputFormatter> _formatters;
        private readonly Func<Stream, Encoding, TextReader> _readerFactory;

        /// <summary>
        /// Creates a new <see cref="BodyModelBinder"/>.
        /// </summary>
        /// <param name="formatters">The list of <see cref="IInputFormatter"/>.</param>
        /// <param name="readerFactory">
        /// The <see cref="IHttpRequestStreamReaderFactory"/>, used to create <see cref="System.IO.TextReader"/>
        /// instances for reading the request body.
        /// </param>
        public MappedBodyModelBinder(IModelMetadataProvider modelMetadataProvider, IMapper mapper, IList<IInputFormatter> formatters, IHttpRequestStreamReaderFactory readerFactory)
        {
            if (modelMetadataProvider == null)
            {
                throw new ArgumentNullException(nameof(modelMetadataProvider));
            }

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

            _modelMetadataProvider = modelMetadataProvider;
            _mapper = mapper;
            _formatters = formatters;
            _readerFactory = readerFactory.CreateReader;
        }

        /// <inheritdoc />
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            // Special logic for body, treat the model name as string.Empty for the top level
            // object, but allow an override via BinderModelName. The purpose of this is to try
            // and be similar to the behavior for POCOs bound via traditional model binding.
            string modelBindingKey;
            if (bindingContext.IsTopLevelObject)
            {
                modelBindingKey = bindingContext.BinderModelName ?? string.Empty;
            }
            else
            {
                modelBindingKey = bindingContext.ModelName;
            }

            var httpContext = bindingContext.HttpContext;

            // Get the mapping source type and its metadata info
            var controllerParameters = bindingContext.ActionContext.ActionDescriptor.Parameters.OfType<ControllerParameterDescriptor>();
            ControllerParameterDescriptor mapFromBodyParameter = null;
            MapFromBodyAttribute mapFromBodyAttribute = null;
            foreach (var param in controllerParameters)
            {
                mapFromBodyAttribute = param.ParameterInfo.GetCustomAttribute<MapFromBodyAttribute>();
                if (mapFromBodyAttribute != null)
                {
                    mapFromBodyParameter = param;
                    break;
                }
            }

            // Set the formatter context for the mapped parameter if found

            InputFormatterContext formatterContext = null;
            if (mapFromBodyParameter == null || mapFromBodyAttribute == null)
            {
                formatterContext = new InputFormatterContext(
                    httpContext,
                    modelBindingKey,
                    bindingContext.ModelState,
                    bindingContext.ModelMetadata,
                    _readerFactory);
            }
            else
            {
                var mappedModelMetadata = _modelMetadataProvider.GetMetadataForType(mapFromBodyAttribute.SourceType);
                formatterContext = new InputFormatterContext(
                    httpContext,
                    modelBindingKey,
                    bindingContext.ModelState,
                    mappedModelMetadata,
                    _readerFactory);
            }

            var formatter = (IInputFormatter)null;
            for (var i = 0; i < _formatters.Count; i++)
            {
                if (_formatters[i].CanRead(formatterContext))
                {
                    formatter = _formatters[i];
                    break;
                }
            }

            if (formatter == null)
            {
                var message = string.Format("Unsupported content type '{0}'.", httpContext.Request.ContentType);

                var exception = new UnsupportedContentTypeException(message);
                bindingContext.ModelState.AddModelError(modelBindingKey, exception, bindingContext.ModelMetadata);
                return;
            }

            try
            {
                var previousCount = bindingContext.ModelState.ErrorCount;
                var result = await formatter.ReadAsync(formatterContext);
                var model = result.Model;

                if (result.HasError)
                {
                    // Formatter encountered an error. Do not use the model it returned.
                    return;
                }

                if (mapFromBodyParameter != null && mapFromBodyAttribute != null)
                {
                    // Map the model to the desired model in the action
                    model = _mapper.Map(model, mapFromBodyAttribute.SourceType, bindingContext.ModelType);
                }

                bindingContext.Result = ModelBindingResult.Success(model);
                return;
            }
            catch (Exception ex)
            {
                bindingContext.ModelState.AddModelError(modelBindingKey, ex, bindingContext.ModelMetadata);
                return;
            }
        }
    }
}
