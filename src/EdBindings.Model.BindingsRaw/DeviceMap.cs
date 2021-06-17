namespace EdBindings.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DeviceMap
    {
        public string Name { get; set; }

        public List<DeviceControlMap> Controls { get; set; }
    }
}
