using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AudioSwitch.Win.Services.Devices;

namespace AudioSwitch.Win.ViewModels.Switcher
{
    class DeviceViewModel : IDevice, INotifyPropertyChanging, INotifyPropertyChanged
    {
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Id { get; }

        string name;
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    RaisePropertyChanging();
                    name = value;
                    RaisePropertyChanged();
                }
            }
        }

        public DeviceType Type { get; }

        Image image;
        public Image Image
        {
            get => image;
            set
            {
                RaisePropertyChanging();
                image = value;
                RaisePropertyChanged();
            }
        }

        public DeviceViewModel(string id, DeviceType type, string name, Image image)
        {
            Id = id;
            Type = type;

            this.name = name;
            this.image = image;
        }

        void RaisePropertyChanging([CallerMemberName] string propertyName = null)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
