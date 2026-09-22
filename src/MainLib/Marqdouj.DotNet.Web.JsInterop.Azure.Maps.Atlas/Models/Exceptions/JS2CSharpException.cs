using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Exceptions
{
    /// <summary>
    /// When a <see cref="JSException"/> is caught by this library, 
    /// it converts it to this exception and throws it.
    /// </summary>
    public class JS2CSharpException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"><see cref="JSException"/></param>
        public JS2CSharpException(JSException ex) : base(ParseMessage(ex))
        {
            FullMessage = ex.Message;
        }

        private static string ParseMessage(JSException ex)
        {
            var message = ex.Message;
            var index = message.IndexOf("Error:", StringComparison.OrdinalIgnoreCase);

            if (index > -1)
                message = message.Substring(0, index).Replace("\n", " ");

            return message;
        }

        /// <summary>
        /// The full message returned in the <see cref="JSException"/>. Normally includes message and stack combined.
        /// </summary>
        public string FullMessage { get; }

        /// <summary>
        /// Logs the <see cref="FullMessage"/>.
        /// </summary>
        /// <param name="logger"></param>
        public void LogError(ILogger logger) => logger?.LogError(FullMessage);
    }
}
