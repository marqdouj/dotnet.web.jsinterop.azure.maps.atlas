# dotnet.web.jsinterop.azure.maps.atlas

![](https://img.shields.io/badge/Status-Preview%20.NET%2011-yellowgreen)

# A .NET library for working with the Azure Maps SDK in JavaScript interop scenarios.

## NOTE: This library is in preview and is not yet production-ready.

## [Configuration](Documents/Configuration.md)

## [Build Solution](Documents/BuildSolution.md)

## Modules: Accessed using `AtlasInterop`.
- [Data](https://learn.microsoft.com/en-us/javascript/api/azure-maps-control/atlas.data?view=azure-maps-typescript-latest)
  - [BoundingBox](https://learn.microsoft.com/en-us/javascript/api/azure-maps-control/atlas.data.boundingbox?view=azure-maps-typescript-latest)
  - [MercatorPoint](https://learn.microsoft.com/en-us/javascript/api/azure-maps-control/atlas.data.mercatorpoint?view=azure-maps-typescript-latest)
  - [Position](https://learn.microsoft.com/en-us/javascript/api/azure-maps-control/atlas.data.position?view=azure-maps-typescript-latest)
- [Math](https://learn.microsoft.com/en-us/javascript/api/azure-maps-control/atlas.math?view=azure-maps-typescript-latest)
- `Factory` Create/Remove `IJSObjectReference` instances of the atlas.Map.

## Components
- `MapContainer`. Helper component to display the map. Use is not required, but recommended for simple control of the map display.

## Release Notes (Current)
- `11.0.0-Preview-3.4`
   - `IAtlasInterop.Popups`. New module has been added for popup support:

## [Release Notes (All)](Documents/ReleaseNotes.md)
