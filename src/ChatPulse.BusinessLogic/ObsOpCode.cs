using System;
using System.Collections.Generic;
using System.Text;

namespace ChatPulse.BusinessLogic
{
    public enum ObsOpCode
    {
        Hello = 0,                 // OBS → Client
        Identify = 1,              // Client → OBS
        Identified = 2,            // OBS → Client
        Reidentify = 3,            // Client → OBS (update session info)
        Reidentified = 4,          // OBS → Client
        Event = 5,                 // OBS → Client (scene changed, streaming started, etc.)
        Request = 6,               // Client → OBS
        RequestResponse = 7,       // OBS → Client
        RequestBatch = 8,          // Client → OBS
        RequestBatchResponse = 9,   // OBS → Client
    }

}
