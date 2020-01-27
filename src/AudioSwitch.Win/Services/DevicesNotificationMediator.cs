using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioSwitch.CoreAudioApi;
using AudioSwitch.Win.Messages;
using AudioSwitch.Win.Services.Devices;
using DevExpress.Mvvm;

namespace AudioSwitch.Win.Services
{
    class DevicesNotificationMediator : IDisposable
    {
        readonly MMDeviceEnumerator deviceEnumerator;
        readonly MMDeviceNotifyClient deviceNotifyClient;
        readonly IMessenger messenger;

        public DevicesNotificationMediator(MMDeviceEnumerator deviceEnumerator
            , MMDeviceNotifyClient deviceNotifyClient
            , IMessenger messenger)
        {
            this.deviceEnumerator = deviceEnumerator;
            this.deviceNotifyClient = deviceNotifyClient;
            this.messenger = messenger;

            deviceEnumerator.RegisterEndpointNotificationCallback(deviceNotifyClient);

            deviceNotifyClient.DefaultChanged += DeviceNotifyClient_DefaultChanged;
            deviceNotifyClient.DeviceAdded += DeviceNotifyClient_DeviceAdded;
            deviceNotifyClient.DeviceRemoved += DeviceNotifyClient_DeviceRemoved;
        }

        void DeviceNotifyClient_DefaultChanged(object sender, DeviceFlowEventArgs e)
        {
            var deviceType = e.DataFlow == EDataFlow.eCapture ? DeviceType.Recording : DeviceType.Playback;
            var message = new DefaultDeviceChangedMessage(e.DeviceId, deviceType);

            messenger.Send(message);
        }

        void DeviceNotifyClient_DeviceRemoved(object sender, DeviceEventArgs e)
        {
            throw new NotImplementedException();
        }

        void DeviceNotifyClient_DeviceAdded(object sender, DeviceEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
