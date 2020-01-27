using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioSwitch.Win.ViewModels.Switcher
{
    class DevicesQueryResult
    {
        public DeviceViewModel DefaultDevice { get; }
        public DeviceViewModel[] Devices { get; }

        public DevicesQueryResult(DeviceViewModel defaultDevice, DeviceViewModel[] devices)
        {
            DefaultDevice = defaultDevice;
            Devices = devices;
        }
    }
}
