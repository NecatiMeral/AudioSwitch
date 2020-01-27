using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioSwitch.CoreAudioApi;
using AudioSwitch.Win.Model;
using AudioSwitch.Win.Services.Devices;
using AutoMapper;

namespace AudioSwitch.Win.AutoMapper
{
    class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MMDevice, DeviceModel>()
                .ConvertUsing(device => new DeviceModel(device.DataFlow == EDataFlow.eRender ? DeviceType.Playback : DeviceType.Recording
                    , device.ID, device.FriendlyName, device.IconPath, device.Selected));


            CreateMap<DeviceModel, DeviceDto>();
        }
    }
}
