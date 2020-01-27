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
// Adapted for AudioSwitch

using System;
using AudioSwitch.CoreAudioApi.Interfaces;
using System.Runtime.InteropServices;

namespace AudioSwitch.CoreAudioApi
{
    public class AudioEndpointVolume : IDisposable
    {
        readonly IAudioEndpointVolume audioEndPointVolume;
        AudioEndpointVolumeCallback audioEndpointVolumeCallback;

        public event AudioEndpointVolumeNotificationDelegate OnVolumeNotification;

        public AudioEndPointVolumeVolumeRange VolumeRange { get; }

        public EEndpointHardwareSupport HardwareSupport { get; }

        public AudioEndpointVolumeStepInformation StepInformation { get; }

        public AudioEndpointVolumeChannels Channels { get; }

        public float MasterVolumeLevel
        {
            get
            {
                Marshal.ThrowExceptionForHR(audioEndPointVolume.GetMasterVolumeLevel(out float result));
                return result;
            }
            set
            {
                Marshal.ThrowExceptionForHR(audioEndPointVolume.SetMasterVolumeLevel(value, Guid.Empty));
            }
        }

        public float MasterVolumeLevelScalar
        {
            get
            {
                Marshal.ThrowExceptionForHR(audioEndPointVolume.GetMasterVolumeLevelScalar(out float result));
                return result;
            }
            set
            {
                Marshal.ThrowExceptionForHR(audioEndPointVolume.SetMasterVolumeLevelScalar(value, Guid.Empty));
            }
        }

        public bool Mute
        {
            get
            {
                Marshal.ThrowExceptionForHR(audioEndPointVolume.GetMute(out bool result));
                return result;
            }
            set
            {
                Marshal.ThrowExceptionForHR(audioEndPointVolume.SetMute(value, Guid.Empty));
            }
        }

        public void VolumeStepUp()
        {
            Marshal.ThrowExceptionForHR(audioEndPointVolume.VolumeStepUp(Guid.Empty));
        }

        public void VolumeStepDown()
        {
            Marshal.ThrowExceptionForHR(audioEndPointVolume.VolumeStepDown(Guid.Empty));
        }

        internal AudioEndpointVolume(IAudioEndpointVolume realEndpointVolume)
        {
            audioEndPointVolume = realEndpointVolume;
            Channels = new AudioEndpointVolumeChannels(audioEndPointVolume);
            StepInformation = new AudioEndpointVolumeStepInformation(audioEndPointVolume);
            Marshal.ThrowExceptionForHR(audioEndPointVolume.QueryHardwareSupport(out uint HardwareSupp));

            HardwareSupport = (EEndpointHardwareSupport)HardwareSupp;
            VolumeRange = new AudioEndPointVolumeVolumeRange(audioEndPointVolume);
            audioEndpointVolumeCallback = new AudioEndpointVolumeCallback(this);
            Marshal.ThrowExceptionForHR(audioEndPointVolume.RegisterControlChangeNotify(audioEndpointVolumeCallback));
        }

        internal void FireNotification(AudioVolumeNotificationData NotificationData)
        {
            OnVolumeNotification?.Invoke(NotificationData);
        }

        public void Dispose()
        {
            if (audioEndpointVolumeCallback != null)
            {
                Marshal.ThrowExceptionForHR(audioEndPointVolume.UnregisterControlChangeNotify(audioEndpointVolumeCallback));
                audioEndpointVolumeCallback = null;
            }
        }

        ~AudioEndpointVolume()
        {
            Dispose();
        }
    }
}
