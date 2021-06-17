using EdBindings.Model.BindingsRaw.Bindings;

using System;
using System.Diagnostics;
using System.Linq;

namespace EdBindings.Model
{
    [DebuggerDisplay("{Name} {PrimaryDevice}/{PrimaryKey}; {SecondaryDevice}/{SecondaryKey}")]
    public class KeyBindingView
    {
        public string Name { get; set; }

        public string PrimaryDevice { get; set; }

        public string PrimaryKey { get; set; }

        public string SecondaryDevice { get; set; }

        public string SecondaryKey { get; set; }


        public static KeyBindingView MakeKeyBindingView(BindingGroup group, DeviceMap deviceMap)
        {
            var view = new KeyBindingView();

            view.Name = group.Name;

            var primary = (BindingDevice)group.Bindings.First(b => new[] { "Binding", "Primary" }.Contains(b.Name));
            var primaryDeviceMap = deviceMap.FindControlMap(primary);
            var secondary = (BindingDevice)group.Bindings.FirstOrDefault(b => b.Name == "Secondary");
            var secondaryDeviceMap = deviceMap.FindControlMap(secondary);

            view.PrimaryDevice = primaryDeviceMap?.DeviceName ?? primary.Device;
            view.PrimaryKey = primaryDeviceMap?.ControlLabel ?? primary.Key;
            view.SecondaryDevice = secondaryDeviceMap?.DeviceName ?? secondary?.Device;
            view.SecondaryKey = secondaryDeviceMap?.ControlLabel ?? secondary?.Key;

            return view;
        }


    }
}
