using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioSwitch.Win.Messages
{
    class DeviceAddedMessage : IDeviceMessage
    {
        public string DeviceId { get; }

        public DeviceAddedMessage(string deviceId)
        {
            DeviceId = deviceId;
        }
    }
}
