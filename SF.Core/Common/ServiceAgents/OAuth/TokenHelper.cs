using  SF.Core.ServiceAgents.Models;
using  SF.Core.ServiceAgents.Settings;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SF.Core.ServiceAgents.OAuth
{
    public class TokenHelper : ITokenHelper
    {
        private readonly IMemoryCache _cache;
        internal HttpClient _client;

        public TokenHelper(IMemoryCache cache)
        {
            if (cache == null) throw new ArgumentNullException(nameof(cache), $"{nameof(cache)} cannot be null");

            _cache = cache;
            _client = new HttpClient();
        }

        public async Task<TokenReply> ReadOrRetrieveToken(ServiceSettings options)
        {
            TokenReply tokenReplyResult = null;
            var cacheKey = options.OAuthClientId + options.OAuthClientSecret + options.OAuthScope + options.OAuthTokenEndpoint;

            tokenReplyResult = _cache.Get<TokenReply>(cacheKey);

            if (tokenReplyResult == null)
            {
                tokenReplyResult = await RetrieveToken(options.OAuthClientId, options.OAuthClientSecret, options.OAuthScope, options.OAuthTokenEndpoint);

                var cacheExpiration = tokenReplyResult.expires_in - 60;
                cacheExpiration = cacheExpiration > 0 ? cacheExpiration : 0;

                if (cacheExpiration > 0)
                {
                    _cache.Set(cacheKey, tokenReplyResult, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheExpiration) });
                }
            }

            return tokenReplyResult;
        }


        private async Task<TokenReply> RetrieveToken(string clientID, string clientSecret, string scope, string tokenEndpoint)
        {
            TokenReply tokenReply = null;

            var content = $"client_id={clientID}&client_secret={clientSecret}&grant_type=client_credentials{(String.IsNullOrWhiteSpace(scope) ? "" : $"&scope={scope}")}";

            var response = await _client.PostAsync(tokenEndpoint, new StringContent(content));

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    tokenReply = JsonConvert.DeserializeObject<TokenReply>(responseBody);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error parsing token: " + ex.Message, ex);
                }
            }
            else
            {
                throw new Exception("Error retrieving token, response status code: " + response.StatusCode);
            }

            return tokenReply;
        }
    }
}