using Microsoft.Extensions.DependencyInjection;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Configuration
{
    /// <summary>
    /// When creating an instance of the altas.Map, the 'ready' and 'error' events
    /// will automatically be subscibed to using the default method name of 'NotifyMapEvent'.
    /// This class allows you to override the default event name.
    /// </summary>
    public class CreateMapEventNames : ICloneable
    {
        /// <summary>
        /// The event name used for the atlas.Map 'ready' event. Default is 'NotifyMapEvent'.
        /// </summary>
        public string? Ready { get; set; }

        /// <summary>
        /// The event name used for the atlas.Map 'error' event. Default is 'NotifyMapEvent'.
        /// </summary>
        public string? Error { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    /// <summary>
    /// Represents the configuration settings for map integration, including authentication and map-specific options.
    /// </summary>
    /// <remarks>Use this class to specify authentication credentials and additional options required for
    /// connecting to and displaying maps within an application. The settings provided by this class are typically
    /// required for initializing map services or components.</remarks>
    public class MapConfiguration : ICloneable
    {
        /// <summary>
        /// <inheritdoc cref="CreateMapEventNames"/>
        /// </summary>
        public CreateMapEventNames? EventNames { get; set; }

        /// <summary>
        /// The authentication settings used for accessing map services.
        /// </summary>
        /// <remarks>Use this property to configure credentials or tokens required for map service
        /// requests. The settings specified here determine how authentication is handled when connecting to external
        /// map providers.</remarks>
        public AuthenticationOptions AuthOptions { get => field; set => field = value ?? throw new ArgumentNullException(nameof(AuthOptions)); } = new();

        /// <summary>
        /// The default global options used to configure the map's behavior and appearance.
        /// These options can be ovverridden at runtime as needed be assigning new values to the map component.
        /// </summary>
        /// <remarks>Assigning a value to this property allows customization of map features such as
        /// controls, display settings, and interaction modes. If set to <see langword="null"/>, default map options
        /// will be used.</remarks>
        public MapOptions? MapOptions { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            var clone = (MapConfiguration)MemberwiseClone();
            clone.EventNames = (CreateMapEventNames?)EventNames?.Clone();
            clone.AuthOptions = (AuthenticationOptions)AuthOptions.Clone();
            clone.MapOptions = (MapOptions?)MapOptions?.Clone();

            return clone;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class MapConfigurationExtensions
    {
        /// <summary>
        /// Configures <see cref="MapConfiguration"/>, optionally enabling configuration validation.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <param name="config">A delegate that configures the <see cref="MapConfiguration"/> instance used for Azure Maps integration.</param>
        /// <returns>An instance of the resolved <see cref="MapConfiguration"/></returns>
        public static MapConfiguration AddMarqdoujAtlasMaps(this IServiceCollection services, Action<MapConfiguration> config)
        {
            services
                .AddOptions<MapConfiguration>()
                .Configure(config);

            return config.GetConfiguration();
        }

        private static MapConfiguration GetConfiguration(this Action<MapConfiguration> config)
        {
            var c = new MapConfiguration();
            config.Invoke(c);
            return c;
        }
    }

}
