# dotnet.web.jsinterop.azure.maps.atlas
[![.NET](https://img.shields.io/badge/.NET-blueviolet.svg)](https://github.com/dotnet/aspnetcore)
[![MIT License](https://img.shields.io/github/license/marqdouj/dotnet.web.jsinterop.azure.maps.atlas?color=%230b0&style=flat-square)](https://github.com/marqdouj/dotnet.web.jsinterop.azure.maps.atlas/blob/master/LICENSE.txt)
[![NuGet](https://img.shields.io/badge/nuget-repo-blue?logo=nuget)](https://www.nuget.org/packages/Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas/)

# Overview
A .NET library for working with the [Azure Maps SDK](https://learn.microsoft.com/en-us/azure/azure-maps/how-to-use-map-control)
using [JS Interop](https://learn.microsoft.com/en-us/aspnet/core/blazor/javascript-interoperability/).

# Features
In most cases, interactions with the map using this library allow for creation of an
[IJSObjectReference](https://learn.microsoft.com/en-us/dotnet/api/microsoft.jsinterop.ijsobjectreference) 
to the map objects you are working with.
- `AtlasInterop`. All map interactions are done using an instance of this class.
- `IMapObjectReference`. When a reference is requested, an `IMapObjectReference` wrapper class to the reference will be created.
  The reference can be accessed via the `JsReference` property. NOTE: Always ensure this wrapper is disposed when you are done using it.
- `AtlasInterop.Factory` Create/Remove instances of the map. When creating a map instance it is always returned as an `IMapObjectReference` wrapper.
- `Custom JSInterop`. If required, you can create your own custom *.js scripts to work with map using the created `IMapObjectReference` wrappers.

## Demo
See the `Sandbox` project for a working example of the library in action.

## Components
- `MapContainer`. Helper component to display the map. Use is not required, but recommended for simple control of the map display.

## [Configuration](Documents/Configuration.md)

## [Build Solution](Documents/BuildSolution.md)

## [Release Notes (All)](Documents/ReleaseNotes.md)

## Release Notes (Current)
- ### `11.0.1`. 
  - Replace existing 'module task' extensions with new [Extensions Package](https://www.nuget.org/packages/Marqdouj.DotNet.Web.JsInterop.Extensions/).
