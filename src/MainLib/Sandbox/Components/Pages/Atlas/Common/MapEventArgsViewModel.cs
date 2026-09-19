using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events;
using System.Collections.ObjectModel;

namespace Sandbox.Components.Pages.Atlas.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class MapEventArgsViewModelManager
    {
        private readonly List<MapEventArgsViewModel> _items = [];

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxItems"></param>
        public MapEventArgsViewModelManager(int maxItems = 25)
        {
            Items = new ReadOnlyCollection<MapEventArgsViewModel>(_items);
            MaxItems = maxItems;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public void Add(MapEventArgs args)
        {
            _items.Insert(0, new MapEventArgsViewModel(args));
            while (_items.Count > MaxItems)
            {
                _items.RemoveAt(_items.Count - 1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public void Add<TPayload>(MapEventArgs args) where TPayload : class
        {
            _items.Insert(0, new MapEventArgsViewModel<TPayload>(args));
            while (_items.Count > MaxItems)
            {
                _items.RemoveAt(_items.Count - 1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            _items.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyCollection<MapEventArgsViewModel> Items { get; }

        /// <summary>
        /// Maximum number of items to display.
        /// </summary>
        public int MaxItems { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    public class MapEventArgsViewModel(MapEventArgs args)
    {
        /// <summary>
        /// 
        /// </summary>
        public MapEventArgs Args { get; } = args;

        /// <summary>
        /// Serialized version of Args.
        /// </summary>
        public string JSON { get; set; } = args.ToJsonMin();

        /// <summary>
        /// Time the view model was created.
        /// </summary>
        public TimeSpan TimeStamp { get; set; } = new TimeSpan(DateTime.Now.Ticks);

        public object? Payload { get; set; }
    }

    public class MapEventArgsViewModel<TPayload> : MapEventArgsViewModel where TPayload: class
    {
        public new string? JSON { get; }

        public new  TPayload? Payload { get; }

        public MapEventArgsViewModel(MapEventArgs args) : base(args)
        {
            Payload = args.GetPayloadItem<TPayload>(typeof(TPayload).Name.Replace("Payload", "", StringComparison.OrdinalIgnoreCase));
            JSON = Payload?.ToJsonMin();
        }
    }
}
