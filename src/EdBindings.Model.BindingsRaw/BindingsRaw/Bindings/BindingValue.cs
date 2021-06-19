namespace EdBindings.Model.BindingsRaw.Bindings
{
    using System.Linq;
    using System.Xml.Linq;

    /// <summary>
    /// Record BindingValue.
    /// </summary>
    public record BindingValue(string Name, string Value) : Binding(Name)
    {

        /// <summary>
        /// Makes the binding value.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>EdBindings.Model.BindingsRaw.Bindings.Binding.</returns>
        public static Binding MakeBindingValue(XElement element)
        {
            if (!element.Attributes().Any(attribute => attribute.Name == "Value"))
            {
                return null;
            }

            return new BindingValue(
                Name: element.Name.LocalName, 
                Value: element.Attribute("Value").Value);
        }
    }
}
