using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Configuration;

namespace Sandbox.Components.Pages.Atlas.Models
{
    internal class StyleViewModel
    {
        public StyleViewModel(StyleOptions options)
        {
            Options = options;
            ShowFeedbackLink = new(nameof(StyleOptions.ShowFeedbackLink), "Show Feedback Link", options);
            ShowLabels = new(nameof(StyleOptions.ShowLogo), "Show Logo", options);
            ShowLogo = new(nameof(StyleOptions.ShowLabels), "Show Labels", options);
            ShowTileBoundaries = new(nameof(StyleOptions.ShowTileBoundaries), "Show Tile Boundaries", options);

            Items = [ShowFeedbackLink, ShowLabels, ShowLogo, ShowTileBoundaries];

            ShowFeedbackLink.Tooltip = "Specifies if the feedback link should be displayed on the map or not.";
            ShowLabels.Tooltip = "Specifies if the map should display labels.";
            ShowLogo.Tooltip = "Specifies if the Microsoft logo should be hidden or not. If set to true a Microsoft copyright string will be added to the map.";
            ShowTileBoundaries.Tooltip = "Specifies if the map should render an outline around each tile and the tile ID.";
        }

        public StyleOptions Options { get; }
        public BoolPropertyViewModel ShowFeedbackLink { get; }
        public BoolPropertyViewModel ShowLabels { get; }
        public BoolPropertyViewModel ShowLogo { get; }
        public BoolPropertyViewModel ShowTileBoundaries { get; }

        public List<BoolPropertyViewModel> Items { get; }
    }
}
