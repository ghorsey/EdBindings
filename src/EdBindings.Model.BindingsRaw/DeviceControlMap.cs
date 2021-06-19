namespace EdBindings.Model
{
    /// <summary>
    /// Class DeviceControlMap.
    /// </summary>
    public class DeviceControlMap
    {
        /// <summary>
        /// Gets or sets the device identifier.
        /// </summary>
        /// <value>The device identifier.</value>
        public string DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the name of the device.
        /// </summary>
        /// <value>The name of the device.</value>
        public string DeviceName { get; set; }

        /// <summary>
        /// Gets or sets the control label.
        /// </summary>
        /// <value>The control label.</value>
        public string ControlLabel { get; set; }

        /// <summary>
        /// Gets or sets the control value.
        /// </summary>
        /// <value>The control value.</value>
        public string ControlValue { get; set; }
    }
}
