namespace EdBindings.Model
{
    using EdBindings.Model.BindingsRaw.Bindings;

    using System.Collections.Generic;
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
    }
}
