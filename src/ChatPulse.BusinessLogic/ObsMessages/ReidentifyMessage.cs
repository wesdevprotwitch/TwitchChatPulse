using System;
using System.Collections.Generic;
using System.Text;

namespace ChatPulse.BusinessLogic.ObsMessages
{
    public class ReidentifyMessage
    {
        public int RpcVersion { get; set; }
        public uint EventSubscriptions { get; set; }
    }
}
