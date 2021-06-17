namespace EdBindings.Model
{
    using EdBindings.Model.BindingsRaw.Bindings;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DeviceMap
    {
        public string Name { get; set; }

        public List<DeviceControlMap> Controls { get; set; }

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
