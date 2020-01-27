using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioSwitch.Win.Services.Devices
{
    class DeviceDto : IDevice
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string IconPath { get; set; }

        public bool IsDefault { get; set; }

        public DeviceType Type { get; set; }
    }
}
