using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.DataDrivenStyles;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Layers;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Sources;
using Marqdouj.DotNet.Web.JsInterop.GeoJson;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Geolocation
{
    /// <summary>
    /// Manages layers to display geolocations on a map.
    /// </summary>
    public class GeolocationLayers
    {
        private readonly List<string> mapIds = [];

        /// <summary>
        /// 
        /// </summary>
        public GeolocationLayers()
        {
            //Render point or MultiPoint in position Layer.
            PositionLayer.Options = new() { Filter = DDSBuilder.GeometryFilter([new(GeoJson.GeometryType.Point), new(GeoJson.GeometryType.MultiPoint)]) };
            AccuracyLayer.Options = new() { FillColor = "rgba(0, 153, 255, 0.5)" };
        }

        /// <summary>
        /// Layer that displays the geolocation positions.
        /// </summary>
        public SymbolLayer PositionLayer { get; } = new() { Id = "positionLayer" };

        /// <summary>
        /// Layer that displays the geolocation accuracy circle for each position.
        /// </summary>
        public PolygonLayer AccuracyLayer { get; } = new() { Id = "accuracyLayer" };

        /// <summary>
        /// DataSource for the layers.
        /// </summary>
        public DataSource DataSource { get; } = new() { Id = "geolocationSource" };

        /// <summary>
        /// Options for the Accuracy layer. Any changes must be made before adding the layers to the map.
        /// </summary>
        public PolygonLayerOptions AccuracyOptions => AccuracyLayer.Options!;

        /// <summary>
        /// Options for the Position layer. Any changes must be made before adding the layers to the map.
        /// </summary>
        public SymbolLayerOptions PositionOptions => PositionLayer.Options!;

        /// <summary>
        /// Indicates if the layers have been added to the map.
        /// </summary>
        public bool LayersAdded(string mapId) => mapIds.Contains(mapId); 

        /// <summary>
        /// Adds the datasource and layers to the map.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="atlasInterop"></param>
        /// <returns></returns>
        public async Task Add(Map map, IAtlasInterop atlasInterop)
        {
            if (LayersAdded(map.MapId))
                throw new Exception($"Layers have already been added to the map where Id = '{map.MapId}'.");

            await atlasInterop.Sources.Add(map, DataSource);

            AccuracyLayer.Source = DataSource.Id;
            PositionLayer.Source = DataSource.Id;

            //Add the accuracy layer before the position layer so the position is on top of the accuracy circle.
            await atlasInterop.Layers.Add(map, [AccuracyLayer, PositionLayer]);
            mapIds.Add(map.MapId);
        }

        /// <summary>
        /// Removes the datasource and layers from the map.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="atlasInterop"></param>
        /// <returns></returns>
        public async Task Remove(Map map, IAtlasInterop atlasInterop)
        {
            if (!LayersAdded(map.MapId))
                return;

            await atlasInterop.Layers.Remove(map, [PositionLayer, AccuracyLayer]);
            mapIds.Remove(map.MapId);
            await atlasInterop.Sources.Remove(map, DataSource);
        }

        /// <summary>
        /// Adds a map feature representing the specified geographic position to the map and optionally includes
        /// an accuracy indicator for the position.
        /// </summary>
        /// <remarks>If showAccuracy is set to true and a position includes accuracy information, an
        /// additional feature representing the accuracy (such as a circle) is added for that position. The returned
        /// list includes both the position features and any associated accuracy features.</remarks>
        /// <param name="map"></param>
        /// <param name="mapInterop">An object that provides interop functionality for interacting with Azure Maps.</param>
        /// <param name="position">A geographic position to add as feature on the map.</param>
        /// <param name="showAccuracy">true to add accuracy indicators (such as circles) for positions that include accuracy information;
        /// otherwise, false. The default is true.</param>
        /// <returns>A MapFeatureDef object representing the added position and, if applicable, it's accuracy
        /// indicator.</returns>
        public async Task<IFeature> AddPosition(Map map, IAtlasInterop mapInterop, GeolocationPosition position, bool showAccuracy = true)
            => (await AddPositions(map, mapInterop, [position], showAccuracy)).First();

        /// <summary>
        /// Adds map features representing the specified geographic positions to the map and optionally includes
        /// accuracy indicators for each position.
        /// </summary>
        /// <remarks>If showAccuracy is set to true and a position includes accuracy information, an
        /// additional feature representing the accuracy (such as a circle) is added for that position. The returned
        /// list includes both the position features and any associated accuracy features.</remarks>
        /// <param name="map"></param>
        /// <param name="mapInterop">An object that provides interop functionality for interacting with Azure Maps.</param>
        /// <param name="positions">A collection of geographic positions to add as features on the map.</param>
        /// <param name="showAccuracy">true to add accuracy indicators (such as circles) for positions that include accuracy information;
        /// otherwise, false. The default is true.</param>
        /// <returns>A list of MapFeatureDef objects representing the added positions and, if applicable, their accuracy
        /// indicators.</returns>
        public async Task<List<IFeature>> AddPositions(Map map, IAtlasInterop mapInterop, IEnumerable<GeolocationPosition> positions, bool showAccuracy = true)
        {
            var features = new List<IFeature>();

            foreach (var position in positions)
            {
                var point = new Point(new Position(position.Coords!.Longitude, position.Coords!.Latitude));
                var pointDef = new Feature<Point, GeoJsonProperties>(point, []);
                pointDef.Properties!.Add("geolocationType", "position");

                features.Add(pointDef);

                if (showAccuracy)
                {
                    var accuracyDef = new Feature<Point, GeoJsonProperties>(point, []);
                    accuracyDef.Properties!.Add("geolocationType", "accuracy");
                    accuracyDef.Properties.Add("subType", "Circle");
                    accuracyDef.Properties.Add("radius", position.Coords.Accuracy);
                    features.Add(accuracyDef);
                }
            }

            await mapInterop.Features.Add(map, DataSource, features, true);

            return features;
        }

        /// <summary>
        /// Clears the datasource for the layers.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="mapInterop"></param>
        /// <returns></returns>
        public async Task Clear(Map map, IAtlasInterop mapInterop)
        {
            if (LayersAdded(map.MapId))
                await mapInterop.Sources.DataSource.Clear(map, DataSource.Id);
        }
    }
}
