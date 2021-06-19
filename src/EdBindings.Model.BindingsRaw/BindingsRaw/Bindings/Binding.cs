namespace EdBindings.Model.BindingsRaw.Bindings
{
    using System;
    using System.Xml.Linq;

    /// <summary>
    /// Record Binding.
    /// </summary>
    public abstract record Binding(string Name)
    {
        /// <summary>
        /// Makes the binding.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>EdBindings.Model.BindingsRaw.Bindings.Binding.</returns>
        public static Binding MakeBinding(XElement element)
        {
            return BindingValue.MakeBindingValue(element)
                 ?? BindingDevice.MakeBindingDevice(element)
                 ?? BindingGroup.MakeBindingGroup(element)
                 ?? throw new InvalidOperationException($"Could not map element {element.Name.LocalName}");

        }

    }
}
