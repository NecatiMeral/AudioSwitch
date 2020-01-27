using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using AudioSwitch.CoreAudioApi.Interfaces;

namespace AudioSwitch.CoreAudioApi
{
    [ComImport, Guid("870af99c-171d-4f9e-af0d-e63df40c2bc9")]
#pragma warning disable IDE1006 // Benennungsstile
    internal class _PolicyConfigClient
#pragma warning restore IDE1006 // Benennungsstile
    {
    }

    public class PolicyConfigClient
    {
        readonly IPolicyConfig policyConfig;
        readonly IPolicyConfigVista policyConfigVista;
        readonly IPolicyConfig10 policyConfig10;

        public PolicyConfigClient()
        {
            policyConfig = new _PolicyConfigClient() as IPolicyConfig;
            if (policyConfig != null)
            {
                return;
            }

            policyConfigVista = new _PolicyConfigClient() as IPolicyConfigVista;
            if (policyConfigVista != null)
            {
                return;
            }

            policyConfig10 = new _PolicyConfigClient() as IPolicyConfig10;
        }

        public void SetDefaultDevice(string deviceID, ERole eRole)
        {
            if (policyConfig != null)
            {
                Marshal.ThrowExceptionForHR(policyConfig.SetDefaultEndpoint(deviceID, eRole));
            }
            else if (policyConfigVista != null)
            {
                Marshal.ThrowExceptionForHR(policyConfigVista.SetDefaultEndpoint(deviceID, eRole));
            }
            else if (policyConfig10 != null)
            {
                Marshal.ThrowExceptionForHR(policyConfig10.SetDefaultEndpoint(deviceID, eRole));
            }
        }
    }
}
