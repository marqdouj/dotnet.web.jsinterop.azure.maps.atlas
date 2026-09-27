using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Configuration;

namespace Sandbox.Components.Pages.Atlas.Models
{
    internal class UserInteractionViewModel
    {
        public UserInteractionViewModel(UserInteractionOptions options)
        {
            Options = options;
            BoxZoomInteraction = new(nameof(UserInteractionOptions.BoxZoomInteraction), "Box Zoom Interaction", options);
            DblClickZoomInteraction = new(nameof(UserInteractionOptions.DblClickZoomInteraction), "DblClick Zoom Interaction", options);
            DragPanInteraction = new(nameof(UserInteractionOptions.DragPanInteraction), "Drag Pan Interaction", options);
            DragRotateInteraction = new(nameof(UserInteractionOptions.DragRotateInteraction), "Drag Rotate Interaction", options);
            ScrollZoomInteraction = new(nameof(UserInteractionOptions.ScrollZoomInteraction), "Scroll Zoom Interaction", options);

            BoxZoomInteraction.Tooltip = "Whether the Shift + left click and drag will draw a zoom box.";
            DblClickZoomInteraction.Tooltip = "Whether double left click will zoom the map inwards.";
            DragPanInteraction.Tooltip = "Whether left click and drag will pan the map.";
            DragRotateInteraction.Tooltip = "Whether right click and drag will rotate and pitch the map.";
            ScrollZoomInteraction.Tooltip = "Whether the map should zoom on scroll input.";

            Items = [BoxZoomInteraction, DblClickZoomInteraction, DragPanInteraction, DragRotateInteraction, ScrollZoomInteraction];
        }

        public UserInteractionOptions Options { get; }
        public BoolPropertyViewModel BoxZoomInteraction { get; }
        public BoolPropertyViewModel DblClickZoomInteraction { get; }
        public BoolPropertyViewModel DragPanInteraction { get; }
        public BoolPropertyViewModel DragRotateInteraction { get; }
        public BoolPropertyViewModel ScrollZoomInteraction { get; }

        public List<BoolPropertyViewModel> Items { get; }
    }
}
