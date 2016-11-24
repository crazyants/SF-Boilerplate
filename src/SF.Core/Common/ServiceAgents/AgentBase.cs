using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using  SF.Core.ServiceAgents.Settings;
using SF.Core.Errors.Exceptions;
using System.Linq;
using SF.Core.Errors;

namespace SF.Core.ServiceAgents
{
    public abstract class AgentBase : IDisposable
    {
        protected readonly ServiceSettings _settings;

        protected readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings();

        protected HttpClient _client;
        private IServiceProvider _serviceProvider;

        protected AgentBase(IServiceProvider serviceProvider, IOptions<ServiceAgentSettings> options)
        {
            if (options.Value == null)
                throw new ArgumentNullException(nameof(ServiceAgentSettings),
                    $"{nameof(ServiceAgentSettings)} cannot be null.");

            _serviceProvider = serviceProvider;

            var serviceAgentSettings = options.Value;
            _settings = GetServiceSettings(serviceAgentSettings);

            var clientFactory = serviceProvider.GetService<IHttpClientFactory>();
            _client = clientFactory.CreateClient(serviceAgentSettings, _settings);
        }

        private ServiceSettings GetServiceSettings(ServiceAgentSettings serviceAgentSettings)
        {
            if (serviceAgentSettings.Services.Any(s => s.Key == GetType().Name))
            {
                return serviceAgentSettings.Services[GetType().Name];
            }

            if (serviceAgentSettings.Services.Any(s => GetType().Name.Contains(s.Key)))
            {
                return serviceAgentSettings.Services.FirstOrDefault(s => GetType().Name.Contains(s.Key)).Value;
            }

            throw new Exception($"Settings not found for service agent {GetType().Name}");
        }

        protected async Task<T> ParseResult<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode) await ParseJsonError(response);
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync(), _jsonSerializerSettings);
        }

        protected async Task ParseJsonError(HttpResponseMessage response)
        {
            var errorJson = await response.Content.ReadAsStringAsync();
            Error errorResponse = null;

            try
            {
                // if there is a response
                if (errorJson.Length > 0)
                {
                    // Try to get Error object from JSON
                    errorResponse = JsonConvert.DeserializeObject<Error>(errorJson, _jsonSerializerSettings);
                    if (errorResponse?.ExtraParameters?.Any() == false)
                    {
                        // If the json couldn't be parsed -> create new error object with custom json
                        errorResponse.ExtraParameters?.Add("json", errorJson);
                    }
                }
            }
            catch (Exception)
            {
                errorResponse = new Error
                {
                    Title = "Json parse error exception.",
                    Status = (int) response.StatusCode
                };
                errorResponse.ExtraParameters?.Add("json", errorJson);
            }

            // Throw proper exception based on HTTP status
            var extraParameters = errorResponse?.ExtraParameters?.ToDictionary(x => x.Key, x => new[] { x.Value?.ToString() }.AsEnumerable());
            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound: throw new NotFoundException(messages: extraParameters);

                case HttpStatusCode.BadRequest: throw new ValidationException(messages: extraParameters);

                case HttpStatusCode.Unauthorized: throw new UnauthorizedException(messages: extraParameters);

                default: throw new ServiceAgentException(messages: extraParameters);
            }
        }

        protected async Task<T> GetAsync<T>(string requestUri)
        {
            var response = await _client.GetAsync(requestUri);
            if (!response.IsSuccessStatusCode) await ParseJsonError(response);
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync(), _jsonSerializerSettings);
        }

        protected async Task<string> GetStringAsync(string requestUri)
        {
            var response = await _client.GetAsync(requestUri);
            if (!response.IsSuccessStatusCode) await ParseJsonError(response);
            return await response.Content.ReadAsStringAsync();
        }

        protected async Task<T> PostAsync<T>(string requestUri, T item)
        {
            HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(item, _jsonSerializerSettings), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(requestUri, contentPost);
            return await ParseResult<T>(response);
        }

        protected async Task<TReponse> PostAsync<TRequest, TReponse>(string requestUri, TRequest item)
        {
            HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(item, _jsonSerializerSettings), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(requestUri, contentPost);
            return await ParseResult<TReponse>(response);
        }



        protected async Task<T> PutAsync<T>(string requestUri, T item)
        {
            HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(item, _jsonSerializerSettings), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(requestUri, contentPost);
            return await ParseResult<T>(response);
        }

        protected async Task<TReponse> PutAsync<TRequest, TReponse>(string requestUri, TRequest item)
        {
            HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(item, _jsonSerializerSettings), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(requestUri, contentPost);
            return await ParseResult<TReponse>(response);
        }

        protected async Task PutWithEmptyResultAsync<T>(string requestUri, T item)
        {
            HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(item, _jsonSerializerSettings), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(requestUri, contentPost);
            if (!response.IsSuccessStatusCode)
                await ParseJsonError(response);
        }

        protected async Task DeleteAsync(string requestUri)
        {
            var response = await _client.DeleteAsync(requestUri);
            if (!response.IsSuccessStatusCode)
                await ParseJsonError(response);
        }

        public void Dispose()
        {
            if (_client != null)
                _client.Dispose();
        }
    }
}