using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioSwitch.CoreAudioApi;
using AudioSwitch.Win.Properties;
using AudioSwitch.Win.Services.Devices;
using AutoMapper;
using DevExpress.Mvvm;
using Microsoft.Extensions.Options;

namespace AudioSwitch.Win.Services.Device
{
    class DevicesService
    {
        readonly Settings settings;
        readonly DevicesRepository devicesRepository;
        readonly PolicyConfigClient policyConfigClient;
        readonly IMessenger messenger;
        readonly IMapper mapper;

        public DevicesService(Settings settings
            , DevicesRepository devicesRepository
            , PolicyConfigClient policyConfigClient
            , IMessenger messenger
            , IMapper mapper)
        {
            this.settings = settings;
            this.devicesRepository = devicesRepository;
            this.policyConfigClient = policyConfigClient;
            this.messenger = messenger;
            this.mapper = mapper;
        }

        public List<DeviceDto> GetAllPlaybackDevices()
        {
            var devices = devicesRepository.FindPlayBackDevices();
            return mapper.Map<List<DeviceDto>>(devices);
        }

        public List<DeviceDto> GetAllRecordingDevices()
        {
            var devices = devicesRepository.FindCaptureDevices();
            return mapper.Map<List<DeviceDto>>(devices);
        }

        public DeviceDto GetDefaultCaptureDevice()
        {
            var device = devicesRepository.GetDefaultCaptureDevice();
            return mapper.Map<DeviceDto>(device);
        }

        public DeviceDto GetDefaultPlaybackDevice()
        {
            var device = devicesRepository.GetDefaultPlaybackDevice();
            return mapper.Map<DeviceDto>(device);
        }

        public DeviceDto GetDevice(string deviceId)
        {
            var device = devicesRepository.GetDevice(deviceId);
            return device != null
                ? mapper.Map<DeviceDto>(device)
                : null;
        }

        public void SetDefaultDevice(IDevice device)
        {
            var currentDefaultDevice = device.Type == DeviceType.Recording
                ? GetDefaultCaptureDevice()
                : GetDefaultPlaybackDevice();

            if (currentDefaultDevice?.Id == device?.Id)
            {
                return;
            }

            policyConfigClient.SetDefaultDevice(device.Id, ERole.eMultimedia);
            if (settings.DefaultMultimediaAndComm)
            {
                policyConfigClient.SetDefaultDevice(device.Id, ERole.eCommunications);
            }
        }
    }
}
