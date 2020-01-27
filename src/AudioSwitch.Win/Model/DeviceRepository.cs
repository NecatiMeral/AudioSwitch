using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioSwitch.CoreAudioApi;
using AudioSwitch.Win.Model;
using AutoMapper;

namespace AudioSwitch.Win
{
    class DevicesRepository
    {
        readonly MMDeviceEnumerator deviceEnumerator;
        readonly IMapper mapper;

        public DevicesRepository(MMDeviceEnumerator deviceEnumerator, IMapper mapper)
        {
            this.deviceEnumerator = deviceEnumerator;
            this.mapper = mapper;
        }

        public IEnumerable<DeviceModel> FindAll()
        {
            var deviceList = new List<MMDevice>();
            deviceList.AddRange(GetDevicesImpl(EDataFlow.eCapture));
            deviceList.AddRange(GetDevicesImpl(EDataFlow.eRender));
            return mapper.Map<List<DeviceModel>>(deviceList);
        }

        public List<DeviceModel> FindCaptureDevices()
        {
            var deviceList = new List<MMDevice>();
            deviceList.AddRange(GetDevicesImpl(EDataFlow.eCapture));
            return mapper.Map<List<DeviceModel>>(deviceList);
        }

        public List<DeviceModel> FindPlayBackDevices()
        {
            var deviceList = new List<MMDevice>();
            deviceList.AddRange(GetDevicesImpl(EDataFlow.eRender));
            return mapper.Map<List<DeviceModel>>(deviceList);
        }

        public DeviceModel GetDefaultCaptureDevice()
        {
            var device = deviceEnumerator.GetDefaultAudioEndpoint(EDataFlow.eCapture, ERole.eMultimedia);
            return mapper.Map<DeviceModel>(device);
        }

        public DeviceModel GetDevice(string deviceId)
        {
            try
            {
                var device = deviceEnumerator.GetDevice(deviceId);
                return mapper.Map<DeviceModel>(device);
            }
            catch
            {
                return null;
            }
        }

        public DeviceModel GetDefaultPlaybackDevice()
        {
            var device = deviceEnumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
            return mapper.Map<DeviceModel>(device);
        }

        IEnumerable<MMDevice> GetDevicesImpl(EDataFlow type)
        {
            var devices = deviceEnumerator.EnumerateAudioEndPoints(type, EDeviceState.Active);
            var devCount = devices.Count;

            for (var i = 0; i < devCount; i++)
            {
                yield return devices[i];
            }
        }
    }
}
