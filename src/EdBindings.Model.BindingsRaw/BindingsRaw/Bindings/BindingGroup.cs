namespace EdBindings.Model.BindingsRaw.Bindings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    public record BindingGroup(string Name, List<Binding> Bindings) : Binding(Name)
    {
        public static Binding MakeBindingGroup(XElement element)
        {
            if (!element.HasElements)
            {
                return null;
            }

            return new BindingGroup(
                Name: element.Name.LocalName,
                Bindings: element.Descendants().Select(element => MakeBinding(element)).ToList());
        }
    }
}
