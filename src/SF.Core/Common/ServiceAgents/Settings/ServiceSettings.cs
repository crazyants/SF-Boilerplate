using System;

namespace SF.Core.ServiceAgents.Settings
{
    public class ServiceSettings
    {
        public string AuthScheme { get; set; } = Defaults.ServiceSettings.AuthScheme;
        public string Scheme { get; set; } = Defaults.ServiceSettings.Scheme;
        public string Host { get; set; }
        public string Port { get; set; }
        public string Path { get; set; } = Defaults.ServiceSettings.Path;

        public string OAuthPathAddition { get; set; }

        public string OAuthClientId { get; set; }

        public string OAuthClientSecret { get; set; }
        public string OAuthScope { get; set; }


        /// <summary>
        /// A bool to indicate if the globaly defined api key should be used for this service agent when the AuthScheme is set to 'ApiKey'. 
        /// </summary>
        public bool UseGlobalApiKey { get; set; }

        /// <summary>
        /// The api key to use for this service agent when the AuthScheme is set to 'ApiKey' and the UseGlobalApiKey is set to 'false'. 
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// The string used as header name for the api key. Default = "ApiKey".
        /// </summary>
        public string ApiKeyHeaderName { get; set; } =  SF.Core.ServiceAgents.Settings.AuthScheme.ApiKey;

        /// <summary>
        /// The user name used for basic authentication scheme.
        /// </summary>
        public string BasicAuthUserName { get; set; }

        /// <summary>
        /// The password used for basic authentication scheme.
        /// </summary>
        public string BasicAuthPassword { get; set; }

        public string Url
        {
            get
            {
                return $"{Scheme}://{Host}{(String.IsNullOrWhiteSpace(Port) ? "" : $":{ Port}")}/{Path}{(String.IsNullOrWhiteSpace(Path) ? "" : "/")}";
            }
        }

        public string OAuthTokenEndpoint
        {
            get
            {
                return $"{Url}{OAuthPathAddition}";
            }
        }

    }
}
