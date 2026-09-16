using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Configuration;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Layers;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Sources;
using Marqdouj.DotNet.Web.JsInterop.GeoJson;
using Sandbox.Services;

namespace Sandbox.Components.Pages.Atlas
{
    internal static class DataExtensions
    {
        #region AddDefaultFeatures()

        public static async Task<List<string>> AddDefaultFeatures(this LayerType layerType, IAtlasInterop atlasInterop, IMapDataService dataService, Map map, MapSource source)
        {
            switch (layerType)
            {
                case LayerType.Bubble:
                    var data = await dataService.GetBubbleLayerData();
                    var p = new GeoJsonProperties { { "title", "my default bubble layer" } };
                    var feature = new Feature<MultiPoint, GeoJsonProperties>(new MultiPoint(data), p);
                    var featureIDs = await atlasInterop.Features.Add(map, source.Id, [feature]);

                    var camera = new CameraOptions
                    {
                        Center = data[1],
                        Zoom = 12,
                        Pitch = 0,
                    };
                    await atlasInterop.Map.SetCamera(map, camera);

                    return featureIDs;
                case LayerType.HeatMap:
                case LayerType.Image:
                case LayerType.Line:
                case LayerType.Polygon:
                case LayerType.PolygonExtrusion:
                case LayerType.Symbol:
                case LayerType.Tile:
                default:
                    return [];
            }
        }

        #endregion

        #region GetDefaultSource

        /// <summary>
        /// It may be used with the SymbolLayer, LineLayer, PolygonLayer, BubbleLayer, and HeatMapLayer.
        /// </summary>
        /// <param name="layerType"></param>
        /// <returns></returns>
        public static MapSource? GetDefaultSource(this LayerType layerType)
        {
            return layerType switch
            {
                LayerType.Image or LayerType.Tile => null,
                _ => new DataSource(),
            };
        }

        #endregion

        #region GetDefaultLayer

        public static async Task<MapLayer> GetDefaultLayer(this LayerType layerType, IMapDataService dataService)
        {
            return layerType switch
            {
                LayerType.Bubble => new BubbleLayer(),
                LayerType.HeatMap => new HeatMapLayer(),
                LayerType.Image => await dataService.GetDefaultImageLayer(),
                LayerType.Line => new LineLayer()
                {
                    Before = "labels",
                    Options = new()
                    {
                        StrokeColor = "Blue",
                        StrokeWidth = 4,
                    }
                },
                LayerType.Polygon => new PolygonLayer()
                {
                    Options = new()
                    {
                        FillColor = "Red",
                        FillOpacity = 0.7,
                    }
                },
                LayerType.PolygonExtrusion => new PolygonExtrusionLayer()
                {
                    Options = new()
                    {
                        FillColor = "Red",
                        FillOpacity = 0.7,
                        Height = 500,
                    }
                },
                LayerType.Symbol => new SymbolLayer() { Options = new() { IconOptions = new() { Image = SymbolIconImage.Pin_Red } } },
                LayerType.Tile => new TileLayer()
                {
                    Options = new()
                    {
                        Opacity = 0.8,
                        TileSize = 256,
                        MinSourceZoom = 7,
                        MaxSourceZoom = 17,
                        TileUrl = await dataService.GetTileLayerUrl(),
                    },
                },
                _ => throw new ArgumentOutOfRangeException(nameof(layerType)),
            };
        }

        private static async Task<ImageLayer> GetDefaultImageLayer(this IMapDataService dataService)
        {
            var layerDef = new ImageLayer();

            var data = await dataService.GetImageLayerData();
            layerDef.Options = new ImageLayerOptions
            {
                Url = data.Url,
                Coordinates = data.Coordinates
            };

            return layerDef;
        }

        #endregion
    }
}
