using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioSwitch.Win.Messages;
using AudioSwitch.Win.Services;
using AudioSwitch.Win.Services.Device;
using AudioSwitch.Win.Services.Devices;
using AudioSwitch.Win.ViewModels.Switcher;
using DevExpress.Mvvm;

namespace AudioSwitch.Win.ViewModels.Devices
{
    abstract class DevicesViewModel : ViewModelBase
    {
        readonly DevicesService devicesService;
        readonly DeviceIconProvider deviceIconProvider;
        readonly IMessenger messenger;
        readonly DeviceType deviceType;

        public BindingList<DeviceViewModel> Devices
        {
            get => GetProperty(() => Devices);
            set => SetProperty(() => Devices, value);
        }

        public DeviceViewModel DefaultDevice
        {
            get => GetProperty(() => DefaultDevice);
            set
            {
                if (value != null)
                {
                    SetProperty(() => DefaultDevice, value, OnDefaultDeviceSet);
                }
            }
        }

        public AsyncCommand UpdateDevices { get; }

        public DevicesViewModel(DevicesService devicesService
            , DeviceIconProvider deviceIconProvider
            , IMessenger messenger
            , DeviceType deviceType)
        {
            this.devicesService = devicesService;
            this.deviceIconProvider = deviceIconProvider;
            this.messenger = messenger;
            this.deviceType = deviceType;

            Devices = new BindingList<DeviceViewModel>();
            UpdateDevices = new AsyncCommand(UpdateDevicesImpl);

            messenger.Register<DefaultDeviceChangedMessage>(this, OnDefaultDeviceChanged);
        }

        public async Task UpdateDevicesImpl()
        {
            var result = await Task.Run(() =>
            {
                var deviceDtos = (deviceType == DeviceType.Recording
                    ? devicesService.GetAllRecordingDevices()
                    : devicesService.GetAllPlaybackDevices()).ToArray();
                var defaultDeviceId = deviceDtos.FirstOrDefault(x => x.IsDefault)?.Id;
                var deviceViewModels = new DeviceViewModel[deviceDtos.Length];

                for (int i = 0; i < deviceDtos.Length; i++)
                {
                    var device = deviceDtos[i];
                    var image = deviceIconProvider.GetIcon(device.IconPath).ToBitmap();

                    deviceViewModels[i] = new DeviceViewModel(device.Id, deviceType, device.Name, image);
                }
                var defaultDevice = deviceViewModels.FirstOrDefault(x => x.Id == defaultDeviceId);

                return new DevicesQueryResult(defaultDevice, deviceViewModels);
            }).ConfigureAwait(true);

            Devices.Clear();
            foreach (var item in result.Devices)
            {
                Devices.Add(item);
            }
            DefaultDevice = result.DefaultDevice;
        }

        void OnDefaultDeviceChanged(DefaultDeviceChangedMessage message)
        {
            var defaultDevice = devicesService.GetDevice(message.DeviceId);
            if (message.Type == deviceType)
            {
                DefaultDevice = Devices.FirstOrDefault(x => x.Id == defaultDevice?.Id);
            }
        }

        async void OnDefaultDeviceSet(DeviceViewModel previousDevice)
        {
            if (previousDevice == null)
            {
                return;
            }

            await Task.Run(() =>
            {
                var device = DefaultDevice;
                devicesService.SetDefaultDevice(device);
            }).ConfigureAwait(true);
        }
    }
}
