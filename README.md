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
- `11.0.0-Preview-3.5`
   - `IMapObjectReference`. Modules have been refactored to make it easier to create references.
   - `JS2CSharpException`. When this library catches a [JSException](https://learn.microsoft.com/en-us/dotnet/api/microsoft.jsinterop.jsexception)
	  it will create an instance of the JS2CSharpException exception and throw that.
   - `Lazy<Task<IJSObjectReference>>`. Extension methods have been added to wrap method calls in a try/catch to handle JSException and process the catch with `JS2CSharpException` .
	  - `InvokeAsyncTC<T>`.
	  - `InvokeVoidAsyncTC`.
   - `Custom (JSInterop)`.
	  - `Sandbox.CustomJs` library has been added that contains a custom script.
	  - `Controls`. New demo page that uses the custom script.

## [Release Notes (All)](Documents/ReleaseNotes.md)
