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
- `11.0.0-Preview-4.0`
  - `IAtlasImageSprite`. New module has been added for image sprite support.
  - `IAtlasAnimations`. New module has been added for animation support.
  - `Sandbox`: Added new demos, and rename/re-organize the nav menu and change some of the page names.
	- `Images`.
	  - `Sprites`. Demonstrates working with image sprites.
	- `Scripts (Optional)`:
	   - `Animations`. 
	      - `Shapes`. Demonstrates how to animate moving a shape on the map.

## [Release Notes (All)](Documents/ReleaseNotes.md)
