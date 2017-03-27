namespace SF.Web.Models
{
    internal class ErrorMessages
    {
        public const string MaxLengthField = "{0} cannot be longer than {1}.";
        public const string MvcNotRegisteredNoActionsDescriptorProvider = "The ActionDescriptorsProvider is not yet registered. Does app.UseCodetableDiscovery get called after app.UseMvc?";
        public const string MvcNotRegisteredNoControllers = "The controllers are not yet registered.  Does app.UseCodetableDiscovery get called after app.UseMvc?";
        public const string ProviderNotRegistered = "The ICodetableProvider is not registered. Does 'services.AddCodetableDiscovery' get called in the ConfigureServices method of the Startup class?";
        public const string RouteBuilderNotRegistered = "The ICodetableDiscoveryRouteBuilder is not registered. Does 'services.AddCodetableDiscovery' get called in the ConfigureServices method of the Startup class?";
        public const string RequiredField = "{0} is required.";
    }
}
