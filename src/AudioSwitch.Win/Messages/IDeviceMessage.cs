using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioSwitch.Win.Messages
{
    interface IDeviceMessage
    {
        string DeviceId { get; }
    }
}
