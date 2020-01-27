using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using DevExpress.Utils;
using DevExpress.Utils.Drawing.Helpers;

namespace AudioSwitch.Win.Services
{
    internal class DeviceIconProvider
    {
        readonly IMessenger messenger;

        public DeviceIconProvider(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        internal Icon GetIcon(string iconPath)
        {
            try
            {
                var path32 = iconPath;
                var path64 = iconPath.ToLowerInvariant().Replace("\\system32\\", "\\sysnative\\");

                path32 = Environment.ExpandEnvironmentVariables(path32);
                path64 = Environment.ExpandEnvironmentVariables(path64);

                var iconAdr32 = path32.Split(',');
                var iconAdr64 = path64.Split(',');
                var indx = "";

                string finalPath;
                Icon icon;

                if (File.Exists(iconAdr32[0]))
                {
                    finalPath = iconAdr32[0];

                    if (iconAdr32.Length > 1)
                    {
                        indx = iconAdr32[1];
                    }
                }
                else if (File.Exists(iconAdr64[0]))
                {
                    finalPath = iconAdr64[0];

                    if (iconAdr64.Length > 1)
                    {
                        indx = iconAdr64[1];
                    }
                }
                else
                {
                    return SystemIcons.Warning;
                }

                if (!string.IsNullOrEmpty(indx))
                {
                    var hIconEx = new IntPtr[1];
                    _ = NativeMethods.ExtractIconEx(finalPath, int.Parse(indx), hIconEx, null, 1);
                    icon = Icon.FromHandle(hIconEx[0]);
                }
                else
                {
                    icon = new Icon(finalPath, 32, 32);
                }

                return icon;
            }
            catch
            {
                return SystemIcons.Warning;
            }
        }
    }
}
