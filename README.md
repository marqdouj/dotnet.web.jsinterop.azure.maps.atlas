# dotnet.web.jsinterop.azure.maps.atlas

![](https://img.shields.io/badge/Status-Preview%20.NET%2011-yellowgreen)

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

## Release Notes (Current)
- `11.0.0-Preview-4.1`
  - `Data-Driven Styles`. New modules have been added for data-driven style support.
	- `DDSBuilder`. A static class for creating data-driven style expressions. Wraps the data-driven style modules.
	- `CaseGetStyleBuilder`. A builder class for constructing case style expressions in Azure Maps.
	- `CoalesceGetStyleBuilder`. A builder class for constructing coalesce style expressions in Azure Maps.
	- `ConcatGetStyleBuilder`. A builder class for constructing concat get style expressions in Azure Maps.
	- `PropertyActionStyleBuilder`. A builder class for creating an action to get a property value in Azure Maps.
	- `GeometryFilterStyleBuilder`. A builder class for constructing geometry filter style expressions in Azure Maps.
	- `LinearInterpolateStyleBuilder`. A builder class for constructing linear interpolate style expressions in Azure Maps.
	- `MatchStyleBuilder`. A builder class for constructing match style expressions in Azure Maps.
	- `StepStyleBuilder`. A builder class for constructing step style expressions in Azure Maps.
  - `SymbolIconOptions`.
	- `RotationSpecification`. Removed `RotationSpecification` property. Use `Rotation` instead.
  - `Sandbox`: Added new demo pages for data-driven styles.

## [Release Notes (All)](Documents/ReleaseNotes.md)
