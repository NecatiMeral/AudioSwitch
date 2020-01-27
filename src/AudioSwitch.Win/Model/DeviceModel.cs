using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AudioSwitch.Win.Services.Devices;

namespace AudioSwitch.Win.Model
{
    class DeviceModel
    {
        public DeviceType Type { get; }

        public string Id { get; }

        public string Name { get; }

        public string IconPath { get; }

        public bool IsDefault { get; }

        public DeviceModel(DeviceType type, string id, string name, string iconPath, bool isDefault)
        {
            Type = type;
            Id = id;
            Name = name;
            IconPath = iconPath;
            IsDefault = isDefault;
        }
    }
}
