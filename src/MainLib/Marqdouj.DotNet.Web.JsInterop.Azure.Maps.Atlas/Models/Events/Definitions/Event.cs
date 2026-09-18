namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Definitions
{
    /// <summary>
    /// Base class for event definitions.
    /// </summary>
    public abstract class Event
    {
        /// <summary>
        /// <see cref="MapEventTarget"/>
        /// </summary>
        public abstract MapEventTarget Target { get; }

        /// <summary>
        /// The name used for JSInvokable callback from JS to .NET . Default is 'NotifyMapEvent'.
        /// </summary>
        public string? EventName { get; set; }

        /// <summary>
        /// If true and the js event supports it, preventDefault will be applied to the event
        /// i.e. Mouse, Touch, and Wheel events. Default is 'true'.
        /// </summary>
        public bool PreventDefault { get; set; } = true;

        /// <summary>
        /// If true adds the event once (for events that support 'once'); otherwise continuous./>.
        /// </summary>
        public bool Once { get; set; }
    }
}
