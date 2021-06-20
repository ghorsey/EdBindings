namespace EdBindings.Model
{
    using EdBindings.Model.BindingsRaw.Bindings;

    using Newtonsoft.Json;

    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Class DeviceMap.
    /// </summary>
    public class DeviceMap
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the controls.
        /// </summary>
        /// <value>The controls.</value>
        public List<DeviceControlMap> Controls { get; set; }

        /// <summary>
        /// Finds the control map.
        /// </summary>
        /// <param name="binding">The binding.</param>
        /// <returns>DeviceControlMap.</returns>
        public DeviceControlMap FindControlMap(BindingDevice binding)
        {
            if (binding == null)
            {
                return null;
            }

            return this.Controls.FirstOrDefault(c => c.DeviceId.ToUpperInvariant() == binding.Device.ToUpperInvariant() && c.ControlValue.ToUpperInvariant() == binding.Key.ToUpperInvariant());
        }

        /// <summary>
        /// Opens the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>DeviceMap.</returns>
        public static DeviceMap Open(string path)
        {
            return JsonConvert.DeserializeObject<DeviceMap>(File.ReadAllText(path));
        }
    }
}
