namespace EdBindings.Model.BindingsRaw.Bindings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    public record BindingDevice(string Name, string Device, string Key) : Binding(Name)
    {

        public static Binding MakeBindingDevice(XElement element)
        {
            if(!element.Attributes().Any(attribute => attribute.Name == "Device"))
            {
                return null;
            }

            return new BindingDevice(
                Name: element.Name.LocalName,
                Device: element.Attribute("Device").Value,
                Key: element.Attribute("Key").Value);
        }
    }
}
