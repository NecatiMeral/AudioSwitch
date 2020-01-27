using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioSwitch.Win.Messages
{
    class DeviceRemovedMessage : IDeviceMessage
    {
        public string DeviceId { get; }

        public DeviceRemovedMessage(string deviceId)
        {
            DeviceId = deviceId;
        }
    }
}
