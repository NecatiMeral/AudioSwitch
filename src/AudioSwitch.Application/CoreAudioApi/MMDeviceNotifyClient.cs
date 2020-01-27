using System;
using System.Collections.Generic;
using System.Text;
using AudioSwitch.CoreAudioApi.Interfaces;

namespace AudioSwitch.CoreAudioApi
{
    public class DeviceFlowEventArgs : DeviceEventArgs
    {
        public EDataFlow DataFlow { get; }

        public DeviceFlowEventArgs(string deviceId, EDataFlow dataFlow)
            : base(deviceId)
        {
            DataFlow = dataFlow;
        }
    }

    public class DeviceEventArgs : EventArgs
    {
        public string DeviceId { get; }

        public DeviceEventArgs(string deviceId)
        {
            DeviceId = deviceId;
        }
    }

    /// <summary>
    /// Used for testing endpoint notification methods.
    /// </summary>
    public class MMDeviceNotifyClient : IMMNotificationClient
    {
        public event EventHandler<DeviceFlowEventArgs> DefaultChanged;
        public event EventHandler<DeviceEventArgs> DeviceAdded;
        public event EventHandler<DeviceEventArgs> DeviceRemoved;

        public void OnDefaultDeviceChanged(EDataFlow dataFlow, ERole deviceRole, string defaultDeviceId)
        {
            DefaultChanged?.Invoke(this, new DeviceFlowEventArgs(defaultDeviceId, dataFlow));
        }

        public void OnDeviceStateChanged(string deviceId, EDeviceState newState)
        {
            var args = new DeviceEventArgs(deviceId);
            if (newState == EDeviceState.Unplugged || newState == EDeviceState.Disabled || newState == EDeviceState.NotPresent)
            {
                DeviceRemoved?.Invoke(this, args);
            }
            else if (newState == EDeviceState.Active)
            {
                DeviceAdded?.Invoke(this, args);
            }
        }

        public void OnDeviceAdded(string deviceId)
        {
            var args = new DeviceEventArgs(deviceId);
            DeviceAdded?.Invoke(this, args);
        }

        public void OnDeviceRemoved(string deviceId)
        {
            var args = new DeviceEventArgs(deviceId);
            DeviceRemoved?.Invoke(this, args);
        }

        public void OnPropertyValueChanged(string deviceId, PROPERTYKEY propertyKey)
        {

        }
    }
}
