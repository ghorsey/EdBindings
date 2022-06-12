namespace EdBindings.Model.BindingsRaw.Bindings
{
    using System;
    using System.Linq;
    using System.Xml.Linq;

    /// <summary>
    /// Record BindingDevice.
    /// </summary>
    public record BindingDevice(string Name, string Device, string Key) : Binding(Name)
    {
        /// <summary>
        /// Makes the binding device.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>EdBindings.Model.BindingsRaw.Bindings.Binding.</returns>
        public static Binding MakeBindingDevice(XElement element)
        {
            if (!element.Attributes().Any(attribute => attribute.Name == "Device"))
            {
                return null;
            }

            return new BindingDevice(
               Name: element.Name.LocalName,
               Device: element.Attribute("Device").Value,
               Key: CreateKeyString(element));
        }

        private static string CreateKeyString(XElement element)
        {
            var key = element.Attribute("Key").Value;

            if (element == null || !element.HasElements)
                return key;

            var modifiers = element.Elements("Modifier");
            return modifiers.Any() ? modifiers.Aggregate(key, (aggregate, mod) => aggregate + " + " + mod.Attribute("Key")?.Value) : key;

        }
    }
}
