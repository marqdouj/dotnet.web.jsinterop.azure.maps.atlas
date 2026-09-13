// For Azure Maps Anonymous authentication
using Microsoft.Identity.Client;

using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Configuration;
using Microsoft.JSInterop;

namespace Sandbox
{
    internal static class MapsSetup
    {
        private static MapConfiguration? mapConfiguration;
        private static string clientSecret = "";
        private static readonly string authorityFormat = "https://login.microsoftonline.com/{0}/oauth2/v2.0";
        private static readonly string graphScope = "https://atlas.microsoft.com/.default";

        public static IServiceCollection ConfigureMarqdoujAzureMaps(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
        {
            //User Secrets for local development; Azure Key Vault for Production?:
            //"AzureMaps": {
            //    "AuthenticationType": "SubscriptionKey",
            //    "AadAppId": "",
            //    "AadTenant": "",
            //    "ClientId": "",
            //    "ClientSecret": "",
            //    "SubscriptionKey": "[YOUR KEY]"
            //  }

            mapConfiguration = services.AddMarqdoujAtlasMaps(config =>
            {
                ConfigureForSubscriptionKey(configuration, config, isDevelopment);
                //ConfigureForSasToken(configuration, config);
                //ConfigureForAad(configuration, config);
                //ConfigureForAnonymous(configuration, config);
            });

            return services;
        }

        private static void ConfigureForSubscriptionKey(IConfiguration configuration, MapConfiguration config, bool isDevelopment)
        {
            config.AuthOptions.AuthType = AuthenticationType.subscriptionKey;
            config.AuthOptions.SubscriptionKey = configuration["AzureMaps:SubscriptionKey"];
        }

        private static void ConfigureForSasToken(IConfiguration configuration, MapConfiguration config)
        {
            config.AuthOptions.AuthType = AuthenticationType.sas;

            config.AuthOptions.SasTokenUrl = configuration["AzureMaps:SasTokenUrl"];
            if (!string.IsNullOrWhiteSpace(config.AuthOptions.SasTokenUrl))
                return;

            config.AuthOptions.SasToken = configuration["AzureMaps:SasToken"];
            if (!string.IsNullOrWhiteSpace(config.AuthOptions.SasToken))
                return;

            config.AuthOptions.TokenInfo = new("Sandbox", "GetSasToken", AuthenticationType.sas);
        }

        /// <summary>
        /// Only used for SasToken AuthOptions.
        /// Requires token callback be configured in App.razor.
        /// </summary>
        /// <returns></returns>
        [JSInvokable]
        public static async Task<string?> GetSasToken()
        {
            //TODO: Implement logic to generate SasToken.
            var sasToken = "[ADD LOGIC TO GET SAS TOKEN]";
            return sasToken;
        }

        private static void ConfigureForAad(IConfiguration configuration, MapConfiguration config)
        {
            config.AuthOptions.AuthType = AuthenticationType.aad;
            config.AuthOptions.AadAppId = configuration["AzureMaps:AadAppId"];
            config.AuthOptions.AadTenant = configuration["AzureMaps:AadTenant"];
            config.AuthOptions.ClientId = configuration["AzureMaps:ClientId"];
        }

        private static void ConfigureForAnonymous(IConfiguration configuration, MapConfiguration config)
        {
            config.AuthOptions.AuthType = AuthenticationType.anonymous;
            config.AuthOptions.AadAppId = configuration["AzureMaps:AadAppId"];
            config.AuthOptions.AadTenant = configuration["AzureMaps:AadTenant"];
            config.AuthOptions.ClientId = configuration["AzureMaps:ClientId"];
            clientSecret = configuration["AzureMaps:ClientSecret"] ?? "";

            config.AuthOptions.TokenInfo = new("Sandbox", "GetAccessToken", AuthenticationType.anonymous);
        }

        /// <summary>
        /// Only used for Anonymous AuthOptions.
        /// Requires token callback be configured in App.razor.
        /// </summary>
        /// <returns></returns>
        [JSInvokable]
        public static async Task<string> GetAccessToken()
        {
            IConfidentialClientApplication daemonClient;
            daemonClient = ConfidentialClientApplicationBuilder.Create(mapConfiguration!.AuthOptions.AadAppId)
                .WithAuthority(string.Format(authorityFormat, mapConfiguration.AuthOptions.AadTenant))
                .WithClientSecret(clientSecret)
                .Build();
            AuthenticationResult authResult =
            await daemonClient.AcquireTokenForClient([graphScope]).ExecuteAsync();
            return authResult.AccessToken;
        }
    }
}
