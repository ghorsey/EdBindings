namespace EdBindings.Model.BindingsRaw
{
    using EdBindings.Model.BindingsRaw.Bindings;

    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public record BindingFile
    {
        public string KeyboardLayout { get; init; }

        public List<Binding> Bindings { get; init; } = new List<Binding>();


        public static BindingFile Open(string bindingFile)
        {
            var document = XElement.Load(bindingFile);

            var bindings = document.Descendants().Where(element => element.Name != "KeyboardLayout").Select(element => Binding.MakeBinding(element)).ToList();

            return new BindingFile()
            {
                KeyboardLayout = document.Descendants("KeyboardLayout").First().Value,
                Bindings = bindings
            };
        }
    }
}
