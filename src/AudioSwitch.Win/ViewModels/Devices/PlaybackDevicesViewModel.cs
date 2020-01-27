using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioSwitch.Win.Services;
using AudioSwitch.Win.Services.Device;
using AudioSwitch.Win.Services.Devices;
using DevExpress.Mvvm;

namespace AudioSwitch.Win.ViewModels.Devices
{
    class PlaybackDevicesViewModel : DevicesViewModel
    {
        public PlaybackDevicesViewModel(DevicesService devicesService, DeviceIconProvider deviceIconProvider, IMessenger messenger)
            : base(devicesService, deviceIconProvider, messenger, DeviceType.Playback)
        {
        }
    }
}
