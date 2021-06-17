namespace EdBindings.Model.BindingsRaw.Bindings
{
    using System;
    using System.Xml.Linq;
    public abstract record Binding(string Name)
    {
        public static Binding MakeBinding(XElement element)
        {
            return BindingValue.MakeBindingValue(element)
                 ?? BindingDevice.MakeBindingDevice(element)
                 ?? BindingGroup.MakeBindingGroup(element)
                 ?? throw new InvalidOperationException($"Could not map element {element.Name.LocalName}");

        }

    }
}
