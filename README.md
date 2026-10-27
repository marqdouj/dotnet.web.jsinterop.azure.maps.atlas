# dotnet.web.jsinterop.azure.maps.atlas
![](https://img.shields.io/badge/Status-Preview%20.NET%2011-yellowgreen)
### [NuGet Package](https://www.nuget.org/packages/Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas/)

# A .NET library for working with the Azure Maps SDK in JavaScript interop scenarios.

## NOTE: This library is in preview and is not yet production-ready.

## [JSInterop](https://learn.microsoft.com/en-us/aspnet/core/blazor/javascript-interoperability/)
In most cases, interactions with the map using this library allow for creation of an
[IJSObjectReference](https://learn.microsoft.com/en-us/dotnet/api/microsoft.jsinterop.ijsobjectreference) 
to the map objects you are working with.
- `AtlasInterop`. All map interactions are done using an instance of this class.
- `IMapObjectReference`. When a reference is requested, an `IMapObjectReference` wrapper class to the reference will be created.
  The reference can be accessed via the `JsReference` property. NOTE: Always ensure this wrapper is disposed when you are done using it.
- `AtlasInterop.Factory` Create/Remove instances of the map. When creating a map instance it is always returned as an `IMapObjectReference` wrapper.
- `Custom JSInterop`. If required, you can create your own custom *.js scripts to work with map using the created `IMapObjectReference` wrappers.

## Demos
In the source code, see the `Sandbox` web app for examples on using this library.

## Components
- `MapContainer`. Helper component to display the map. Use is not required, but recommended for simple control of the map display.

## [Configuration](Documents/Configuration.md)

## [Build Solution](Documents/BuildSolution.md)

## [Release Notes (All)](Documents/ReleaseNotes.md)

## Release Notes (Current)
- `11.0.0-Preview-5.0`
  - `IAtlasInterop.Sources.DataSource`.
	- `ToJson`. New method. Exports the datasource to a feature collection and then saves it as JSON.
	- `ToJsonString`. New method. Exports the datasource to a feature collection and then returns it as a JSON string.
  - `Sandbox`.
	- `Basic/Layers (Basic)`. Update demo to support `ToJson`.
