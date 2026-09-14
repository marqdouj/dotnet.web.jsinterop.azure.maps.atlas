using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

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
            public MapConfiguration GetValue()
                => options.Value ?? throw new Exception($"Injection of {nameof(MapConfiguration)} returned null.");
        }
    }
}
