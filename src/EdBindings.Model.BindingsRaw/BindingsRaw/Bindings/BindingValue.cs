namespace EdBindings.Model.BindingsRaw.Bindings
{
    using System.Linq;
    using System.Xml.Linq;
    public record BindingValue(string Name, string Value) : Binding(Name)
    {

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
