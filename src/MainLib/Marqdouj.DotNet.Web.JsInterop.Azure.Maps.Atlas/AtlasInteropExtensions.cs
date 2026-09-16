using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas
{
    /// <summary>
    /// <see cref="AtlasInterop"/> extensions methods.
    /// </summary>
    public static class AtlasInteropExtensions
    {
        extension(IOptions<MapConfiguration> options)
        {
            /// <summary>
            /// Gets the <see cref="MapConfiguration"/> value from the <see cref="IOptions{TOptions}"/>.
            /// Throws an exception if the value is null.
            /// </summary>
            /// <param name="logLevel">(Optional). Set the log level for the browser. Default is <see cref="LogLevel.Information"/></param>
            /// <returns></returns>
            /// <exception cref="Exception"></exception>
            public MapConfiguration GetValue(LogLevel? logLevel = null)
            {
                var value = options.Value ?? throw new Exception($"Injection of {nameof(MapConfiguration)} returned null.");
                if(logLevel != null)
                    value.JsLogLevel = logLevel;
                return value;
            }
        }
    }
}
