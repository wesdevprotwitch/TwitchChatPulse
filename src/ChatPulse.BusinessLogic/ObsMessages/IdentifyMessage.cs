using System;
using System.Collections.Generic;
using System.Text;

namespace ChatPulse.BusinessLogic.ObsMessages
{
    public class IdentifyMessage
    {
        public int RpcVersion { get; set; }
        public string Authentication { get; set; } = string.Empty;
        public uint EventSubscriptions { get; set; }
    }
}
