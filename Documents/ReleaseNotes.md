## Release Notes

### [<- Go Back](../README.md)
- `11.0.0-Preview-3.5`
   - `IMapObjectReference`. All modules have been refactored to make it easier to create references.
- `11.0.0-Preview-3.4`
   - `IAtlasInterop.Popups`. New module has been added for popup support:
- `11.0.0-Preview-3.3`
   - `IAtlasInterop.Events`. New modules have been added for event support:
	  - Layer, Map, Marker, and StyleControl.
- `11.0.0-Preview-3.2`
   - `IAtlasSource`. 
	  - `Clear` methods has been updated and moved to `IAtlasDataSource`.
	  - `Remove/GetSources`. Method signatures have been updated.
- `11.0.0-Preview-3.1`
   - `Map Controls`. You can now interact with basic map controls via `AtlasInterop.Controls`.
   - `IOptions<MapConfiguration>`. Added extension method `GetValue()`.
   - `MapConfiguration`. Now implements `ICloneable`.
   - `Map Sources`. You can now interact with basic map sources via `AtlasInterop.Sources`.
   - `Map Layers`. You can now interact with basic map layers via `AtlasInterop.Layers`.
- `11.0.0-Preview-3.0`
   - `Map Instances`. You can now create/remove instances of the atlas Map via `AtlasInterop.Factory`.
- `11.0.0-Preview-2.0`
   - `Namespaces`. Changed all namespaces to match folder structure (copy and paste issue).
   - `DistanceProperties`. Renamed `distance` to `Distance`.
- `11.0.0-Preview-1.0`: Initial pre-release.
