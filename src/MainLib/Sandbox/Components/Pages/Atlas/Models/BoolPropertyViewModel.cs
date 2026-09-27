using System.Reflection;

namespace Sandbox.Components.Pages.Atlas.Models
{
    internal class BoolPropertyViewModel(string propertyName, string label, object model, bool defaultValue = true)
    {
        private readonly PropertyInfo info = model.GetType().GetProperty(propertyName) ?? throw new ArgumentOutOfRangeException(nameof(propertyName));
        private readonly bool defaultValue = defaultValue;

        public string Label { get; } = label;
        public object Model { get; } = model;
        public string? Tooltip { get; set; }

        public bool Value
        {
            get
            {
                var current = (bool?)info.GetValue(Model);
                return current ?? defaultValue;
            }
            set
            {
                info.SetValue(Model, value);
            }
        }
    }
}
