using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Common;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.ImageSprite
{
    /// <summary>
    /// Represents the definition of an image template, including its name, identifier, and optional customization
    /// properties such as color and scale.
    /// </summary>
    /// <remarks>Use this class to specify the configuration for an image template that can be applied to map
    /// elements or other visual components. The template name must correspond to a supported template. The identifier
    /// can be provided or automatically generated to ensure uniqueness within CSS contexts. Properties such as Color,
    /// SecondaryColor, and Scale allow further customization of the template's appearance.</remarks>
    public class ImageTemplate : JsInteropIdBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">The image's id. If the specified id matches the id of a previously added image the new image will be ignored.</param>
        /// <param name="templateName"><see cref="TemplateName"/></param>
        public ImageTemplate(string id, string templateName)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(id);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(templateName);
            Id = id;
            TemplateName = templateName;
        }

        /// <summary>
        /// <inheritdoc cref="ImageTemplate(string, string)"/>
        /// </summary>
        /// <param name="id">The image's id. If the specified id matches the id of a previously added image the new image will be ignored.</param>
        /// <param name="templateName"><see cref="TemplateName"/></param>
        public ImageTemplate(string id, ImageTemplateName templateName)
            : this(id, templateName.ToJsonName()) { }

        /// <summary>
        /// Specifies which image template to use.
        /// </summary>
        public string TemplateName { get; set; }

        /// <summary>
        /// The primary color. Default: #1A73AA
        /// </summary>
        public string? Color { get; set; }

        /// <summary>
        /// The secondary color. Default: white
        /// </summary>
        public string? SecondaryColor { get; set; }

        /// <summary>
        /// Specifies how much to scale the template. 
        /// For best results, scale the icon to the maximum size you want to display it on the map, 
        /// then use the symbol layers icon size option to scale down if needed. 
        /// This will reduce blurriness due to scaling. Default: 1
        /// </summary>
        public double? Scale { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
}
