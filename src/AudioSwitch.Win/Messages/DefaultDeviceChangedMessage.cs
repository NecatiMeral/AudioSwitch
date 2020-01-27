using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioSwitch.Win.Services.Devices;

namespace AudioSwitch.Win.Messages
{
    class DefaultDeviceChangedMessage : IDeviceMessage
    {
        public string DeviceId { get; }
        public DeviceType Type { get; }

        public DefaultDeviceChangedMessage(string defaultDeviceId, DeviceType type)
        {
            DeviceId = defaultDeviceId;
            Type = type;
        }
    }
}
