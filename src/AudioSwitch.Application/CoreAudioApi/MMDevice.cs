/*
  LICENSE
  -------
  Copyright (C) 2007-2010 Ray Molenkamp

  This source code is provided 'as-is', without any express or implied
  warranty.  In no event will the authors be held liable for any damages
  arising from the use of this source code or the software it produces.

  Permission is granted to anyone to use this source code for any purpose,
  including commercial applications, and to alter it and redistribute it
  freely, subject to the following restrictions:

  1. The origin of this source code must not be misrepresented; you must not
     claim that you wrote the original source code.  If you use this source code
     in a product, an acknowledgment in the product documentation would be
     appreciated but is not required.
  2. Altered source versions must be plainly marked as such, and must not be
     misrepresented as being the original source code.
  3. This notice may not be removed or altered from any source distribution.
*/
using System;
using AudioSwitch.CoreAudioApi.Interfaces;
using System.Runtime.InteropServices;

namespace AudioSwitch.CoreAudioApi
{
    public class MMDevice
    {
        PropertyStore propertyStore;
        AudioMeterInformation audioMeterInformation;
        AudioEndpointVolume audioEndpointVolume;
        AudioSessionManager2 audioSessionManager2;
        DeviceTopology deviceTopology;

        public AudioSessionManager2 AudioSessionManager2 => audioSessionManager2 ?? (audioSessionManager2 = GetAudioSessionManager2());

        public AudioMeterInformation AudioMeterInformation => audioMeterInformation ?? (audioMeterInformation = GetAudioMeterInformation());

        public AudioEndpointVolume AudioEndpointVolume => audioEndpointVolume ?? (audioEndpointVolume = GetAudioEndpointVolume());

        public PropertyStore Properties => propertyStore ?? (propertyStore = GetPropertyInformation());

        public DeviceTopology DeviceTopology => deviceTopology ?? (deviceTopology = GetDeviceTopology());

        public string FriendlyName => Properties.Contains(PKEY.PKEY_DeviceInterface_FriendlyName)
            ? (string)propertyStore[PKEY.PKEY_DeviceInterface_FriendlyName].Value
            : "Unknown";

        public string IconPath => Properties.Contains(PKEY.PKEY_DeviceClass_IconPath)
            ? (string)Properties[PKEY.PKEY_DeviceClass_IconPath].Value
            : "Unknown";

        public string ID
        {
            get
            {
                Marshal.ThrowExceptionForHR(RealDevice.GetId(out string Result));
                return Result;
            }
        }

        public EDataFlow DataFlow
        {
            get
            {
                var ep = RealDevice as IMMEndpoint;
                ep.GetDataFlow(out EDataFlow Result);
                return Result;
            }
        }

        public EDeviceState State
        {
            get
            {
                Marshal.ThrowExceptionForHR(RealDevice.GetState(out EDeviceState Result));
                return Result;
            }
        }

        internal IMMDevice RealDevice { get; }

        public bool Selected
        {
            get => new MMDeviceEnumerator().GetDefaultAudioEndpoint(DataFlow, ERole.eMultimedia).ID == ID;
            set
            {
                if (value == true)
                {
                    new CPolicyConfigVistaClient().SetDefaultDevie(ID);
                    //if(System.Environment.OSVersion.Version.Major==6 && System.Environment.OSVersion.Version.Minor==0)
                    //    (new CPolicyConfigVistaClient()).SetDefaultDevie(this.ID);
                    //else
                    //    (new CPolicyConfigClient()).SetDefaultDevie(this.ID);
                }
            }
        }

        internal MMDevice(IMMDevice realDevice)
        {
            RealDevice = realDevice;
        }

        PropertyStore GetPropertyInformation()
        {
            Marshal.ThrowExceptionForHR(RealDevice.OpenPropertyStore(EStgmAccess.STGM_READ, out IPropertyStore propstore));
            return new PropertyStore(propstore);
        }

        AudioSessionManager2 GetAudioSessionManager2()
        {
            Marshal.ThrowExceptionForHR(RealDevice.Activate(ref IIDs.IID_IAudioSessionManager2, CLSCTX.ALL, IntPtr.Zero, out object result));
            return new AudioSessionManager2(result as IAudioSessionManager2);
        }

        AudioMeterInformation GetAudioMeterInformation()
        {
            Marshal.ThrowExceptionForHR(RealDevice.Activate(ref IIDs.IID_IAudioMeterInformation, CLSCTX.ALL, IntPtr.Zero, out object result));
            return new AudioMeterInformation(result as IAudioMeterInformation);
        }

        AudioEndpointVolume GetAudioEndpointVolume()
        {
            Marshal.ThrowExceptionForHR(RealDevice.Activate(ref IIDs.IID_IAudioEndpointVolume, CLSCTX.ALL, IntPtr.Zero, out object result));
            return new AudioEndpointVolume(result as IAudioEndpointVolume);
        }

        DeviceTopology GetDeviceTopology()
        {
            Marshal.ThrowExceptionForHR(RealDevice.Activate(ref IIDs.IID_IDeviceTopology, CLSCTX.ALL, IntPtr.Zero, out object result));
            return new DeviceTopology(result as IDeviceTopology);
        }
    }
}
