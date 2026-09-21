using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Common;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Configuration;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Layers;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Sources;
using Marqdouj.DotNet.Web.JsInterop.GeoJson;
using Sandbox.Services;

namespace Sandbox.Components.Pages.Atlas
{
    internal static class DataExtensions
    {
        #region AddDefaultLayer

        public class AddDefaultLayerResults(MapSource? source, ILayer? layer)
        {
            public MapSource? Source { get; } = source;
            public ILayer? Layer { get; } = layer;
        }

        public static async Task<AddDefaultLayerResults> AddDefaultLayer(this LayerType layerType, IAtlasInterop atlasInterop, Map map, IMapDataService dataService)
        {
            LayerType? type = layerType;
            return await type.AddDefaultLayer(atlasInterop, map, dataService);
        }

        public static async Task<AddDefaultLayerResults> AddDefaultLayer(this LayerType? layerType, IAtlasInterop atlasInterop, Map map, IMapDataService dataService)
        {
            var source = layerType.GetDefaultSource();
            var layer =  await layerType.GetDefaultLayer(dataService);
            var results = new AddDefaultLayerResults(source, layer);
            List<IMapObjectReference> references = [];

            if (source != null)
            {
                references = await atlasInterop.Sources.Add(map, [source!], layerType == LayerType.HeatMap);
                layer.Source = source?.Id;
            }

            _ = await layerType.AddDefaultFeatures(atlasInterop, dataService, map, source!);
            _ = await atlasInterop.Layers.Add(map, [layer]);

            if (layerType == LayerType.HeatMap)
            {
                var url = await dataService.GetHeatMapLayerUrl();

                //Testing both id and reference - all passed.
                //layer.Source = source?.Id;
                await atlasInterop!.Sources.DataSource.ImportDataFromUrl(map, (string)layer.Source!, url);
                //layer.Source = references[0];
                //await atlasInterop!.Sources.DataSource.ImportDataFromUrl(map, (IMapObjectReference)layer.Source!, url);
            }

            await references.DisposeItems();
            return results;
        }

        //private static async Task DisposeReferences(this List<IMapObjectReference> references)
        //{
        //    foreach (var mref in references)
        //        await mref.DisposeAsync();
        //}

        #endregion

        #region AddDefaultFeatures()

        private static async Task<List<IMapObjectReference>> AddDefaultFeatures(this LayerType? layerType, IAtlasInterop atlasInterop, IMapDataService dataService, Map map, MapSource source, bool getReferences = false)
        {
            var results = new List<IMapObjectReference>();

            switch (layerType)
            {
                case LayerType.Bubble:
                    results = await AddBubbleLayerFeatures(atlasInterop, dataService, map, source, getReferences);
                    break;
                case LayerType.HeatMap:
                    await atlasInterop.Map.SetCamera(map, new CameraOptions { Center = new Position(-122.33, 47.6), Zoom = 1, Pitch = 0, });
                    break;
                case LayerType.Image:
                    await atlasInterop.Map.SetCamera(map, new CameraOptions { Center = new Position(-74.172363, 40.735657), Zoom = 11, Pitch = 0, });
                    break;
                case LayerType.Line:
                    results = await AddLineLayerFeatures(atlasInterop, dataService, map, source, getReferences);
                    break;
                case LayerType.Polygon:
                    results = await AddPolygonLayerFeatures(atlasInterop, dataService, map, source, getReferences);
                    break;
                case LayerType.PolygonExtrusion:
                    results = await AddPolygonExtrusionLayerFeatures(atlasInterop, dataService, map, source, getReferences);
                    break;
                case LayerType.Symbol:
                    results = await AddSymbolLayerFeatures(atlasInterop, dataService, map, source, getReferences);
                    break;
                case LayerType.Tile:
                    await atlasInterop.Map.SetCamera(map, new CameraOptions { Center = new Position(-122.426181, 47.608070), Zoom = 10.75, Pitch = 0, });
                    break;
                default:
                    return [];
            }

            return results;
        }

        private static async Task<List<IMapObjectReference>> AddBubbleLayerFeatures(IAtlasInterop atlasInterop, IMapDataService dataService, Map map, MapSource source, bool getReferences)
        {
            var data = await dataService.GetBubbleLayerData();
            var p = new GeoJsonProperties { { "title", "my default bubble layer" } };
            var feature = new Feature<MultiPoint, GeoJsonProperties>(new MultiPoint(data), p);
            var items = await atlasInterop.Features.Add(map, source.Id, [feature], getReferences);

            await atlasInterop.Map.SetCamera(map, new CameraOptions { Center = data[1], Zoom = 12, Pitch = 0, });
            return items;
        }

        private static async Task<List<IMapObjectReference>> AddLineLayerFeatures(IAtlasInterop atlasInterop, IMapDataService dataService, Map map, MapSource source, bool getReferences)
        {
            var data = await dataService.GetLineLayerData();
            var p = new GeoJsonProperties { { "title", "my default line layer" } };
            var feature = new Feature<LineString, GeoJsonProperties>(new LineString(data), p);
            var items = await atlasInterop.Features.Add(map, source.Id, [feature], getReferences);

            await atlasInterop.Map.SetCamera(map, new CameraOptions { Center = data[8], Zoom = 11, Pitch = 0, });
            return items;
        }

        private static async Task<List<IMapObjectReference>> AddPolygonLayerFeatures(IAtlasInterop atlasInterop, IMapDataService dataService, Map map, MapSource source, bool getReferences)
        {
            var data = await dataService.GetPolygonLayerData();
            var p = new GeoJsonProperties { { "title", "my default polygon layer" } };
            var feature = new Feature<Polygon, GeoJsonProperties>(new Polygon(data), p);
            var items = await atlasInterop.Features.Add(map, source.Id, [feature], getReferences);

            await atlasInterop.Map.SetCamera(map, new CameraOptions { Center = data[0][0], Zoom = 11, Pitch = 0, });
            return items;
        }

        private static async Task<List<IMapObjectReference>> AddPolygonExtrusionLayerFeatures(IAtlasInterop atlasInterop, IMapDataService dataService, Map map, MapSource source, bool getReferences)
        {
            var data = await dataService.GetPolygonExtLayerData();
            var p = new GeoJsonProperties { { "title", "my default polygon extrusion layer" } };
            var feature = new Feature<Polygon, GeoJsonProperties>(new Polygon(data), p);
            var items = await atlasInterop.Features.Add(map, source.Id, [feature], getReferences);

            await atlasInterop.Map.SetCamera(map, new CameraOptions { Center = data[0][0], Zoom = 11, Pitch = 60, });
            return items;
        }

        private static async Task<List<IMapObjectReference>> AddSymbolLayerFeatures(IAtlasInterop atlasInterop, IMapDataService dataService, Map map, MapSource source, bool getReferences)
        {
            var data = await dataService.GetSymbolLayerData();
            var i = 0;
            var features =new List<object>();

            foreach (var position in data)
            {
                i++;
                var p = new GeoJsonProperties { { "description", $"Symbol {i}" } };
                var feature = new Feature<Point, GeoJsonProperties>(new Point(position), p);
                features.Add(feature);
            }

            var items = await atlasInterop.Features.Add(map, source.Id, features, getReferences);
            await atlasInterop.Map.SetCamera(map, new CameraOptions { Center = data[8], Zoom = 11, Pitch = 0, });
            return items;
        }

        #endregion

        #region GetDefaultSource

        /// <summary>
        /// Get the default source for a layer.
        /// </summary>
        /// <param name="layerType"></param>
        /// <returns></returns>
        private static DataSource? GetDefaultSource(this LayerType? layerType)
        {
            if (layerType == null)
                return null;

            return layerType switch
            {
                LayerType.Image or LayerType.Tile => null,
                _ => new DataSource(),
            };
        }

        #endregion

        #region GetDefaultLayer

        private static async Task<ILayer> GetDefaultLayer(this LayerType? layerType, IMapDataService dataService)
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
