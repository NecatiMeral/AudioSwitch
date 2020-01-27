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

using AudioSwitch.CoreAudioApi.Interfaces;
using System.Runtime.InteropServices;

namespace AudioSwitch.CoreAudioApi
{
    public class DeviceTopology
    {
        readonly IDeviceTopology deviceTopology;

        public int GetConnectorCount
        {
            get
            {
                Marshal.ThrowExceptionForHR(deviceTopology.GetConnectorCount(out int count));
                return count;
            }
        }

        public int GetSubunitCount
        {
            get
            {
                Marshal.ThrowExceptionForHR(deviceTopology.GetSubunitCount(out int count));
                return count;
            }
        }

        public string GetDeviceId
        {
            get
            {
                Marshal.ThrowExceptionForHR(deviceTopology.GetDeviceId(out string id));
                return id;
            }
        }

        internal DeviceTopology(IDeviceTopology realInterface)
        {
            deviceTopology = realInterface;
        }

        public Connector GetConnector(int index)
        {
            Marshal.ThrowExceptionForHR(deviceTopology.GetConnector(index, out IConnector connector));
            return new Connector(connector);
        }

        public Subunit GetSubunit(int index)
        {
            Marshal.ThrowExceptionForHR(deviceTopology.GetSubunit(index, out ISubunit subUnit));
            return new Subunit(subUnit);
        }

        public Part GetPartById(int id)
        {
            Marshal.ThrowExceptionForHR(deviceTopology.GetPartById(id, out IPart part));
            return new Part(part);
        }

        public PartsList GetSignalPath(Part from, Part to, bool rejectMixedPaths)
        {
            Marshal.ThrowExceptionForHR(deviceTopology.GetSignalPath((IPart)from, (IPart)to, rejectMixedPaths, out IPartsList partList));
            return new PartsList(partList);
        }
    }
}
