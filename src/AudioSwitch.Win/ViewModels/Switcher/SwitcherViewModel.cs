using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AudioSwitch.CoreAudioApi;
using AudioSwitch.Win.Messages;
using AudioSwitch.Win.Services;
using AudioSwitch.Win.Services.Device;
using AudioSwitch.Win.Services.Devices;
using AudioSwitch.Win.ViewModels.Devices;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;

namespace AudioSwitch.Win.ViewModels.Switcher
{
    class SwitcherViewModel : ViewModelBase
    {
        public DevicesViewModel Playback { get; }

        public DevicesViewModel Recording { get; }

        public bool Muted
        {
            get => GetProperty(() => Muted);
            set => SetProperty(() => Muted, value);
        }

        public SwitcherViewModel(Func<PlaybackDevicesViewModel> playbackDevicesViewModelFactory
            , Func<RecordingDevicesViewModel> recodingDevicesViewModelFactory)
        {
            Playback = playbackDevicesViewModelFactory();
            Recording = recodingDevicesViewModelFactory();
        }
    }
}
