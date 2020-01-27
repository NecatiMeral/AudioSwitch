using System;
using System.Collections.Generic;
using System.Text;

namespace AudioSwitch.Messages
{
    public class DpiChangedMessage
    {
        public int OldDpi { get; }
        public int NewDPi { get; }

        public DpiChangedMessage(int oldDpi, int newDPi)
        {
            OldDpi = oldDpi;
            NewDPi = newDPi;
        }
    }
}
