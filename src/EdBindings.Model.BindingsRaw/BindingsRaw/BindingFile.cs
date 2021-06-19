namespace EdBindings.Model.BindingsRaw
{
    using EdBindings.Model.BindingsRaw.Bindings;

    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    /// <summary>
    /// Record BindingFile.
    /// </summary>
    public record BindingFile
    {
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; init; }

        /// <summary>
        /// Gets or sets the keyboard layout.
        /// </summary>
        /// <value>The keyboard layout.</value>
        public string KeyboardLayout { get; init; }

        /// <summary>
        /// Gets or sets the bindings.
        /// </summary>
        /// <value>The bindings.</value>
        public List<Binding> Bindings { get; init; } = new List<Binding>();


        /// <summary>
        /// Opens the specified binding file.
        /// </summary>
        /// <param name="bindingFile">The binding file.</param>
        /// <returns>EdBindings.Model.BindingsRaw.BindingFile.</returns>
        public static BindingFile Open(string bindingFile)
        {
            var document = XElement.Load(bindingFile);

            var bindings = document.Descendants().Where(element => element.Name != "KeyboardLayout").Select(element => Binding.MakeBinding(element)).ToList();

            return new BindingFile()
            {
                FileName = bindingFile,
                KeyboardLayout = document.Descendants("KeyboardLayout").First().Value,
                Bindings = bindings
            };
        }
    }
}
